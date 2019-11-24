﻿using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public int size;
        public GameObject prefab;
    }

    #region Singleton
    public static Pooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnPoolObject(string _tag, Vector3 _position, Quaternion _rotation)
    {
        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.Log("Object with tag " + _tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[_tag].Dequeue();

        objectToSpawn.transform.localPosition = _position;
        objectToSpawn.transform.localRotation = _rotation;

        objectToSpawn.SetActive(true);

        IPooled pooledObject = objectToSpawn.GetComponent<IPooled>();

        if(pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        //poolDictionary[_tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public List<GameObject> SpawnEntirePool(string _tag, Queue<Vector3> _positions, Queue<Quaternion> _rotations)
    {
        List<GameObject> objectsToSpawn = new List<GameObject>();

        if (!poolDictionary.ContainsKey(_tag))
        {
            Debug.Log("Object with tag " + _tag + " doesn't exist");
            return null;
        }

        foreach(GameObject objectToSpawn in poolDictionary[_tag])
        {
            objectToSpawn.transform.localPosition = _positions.Dequeue();
            objectToSpawn.transform.localRotation = _rotations.Dequeue();

            objectToSpawn.SetActive(true);

            IPooled pooledObject = objectToSpawn.GetComponent<IPooled>();

            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawn();
            }

            objectsToSpawn.Add(objectToSpawn);
        }

        return objectsToSpawn;
    }
}
