using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotSpawner : MonoBehaviour
{
    private void Start()
    {
        Queue<Vector3> positions = new Queue<Vector3>();
        Queue<Quaternion> rotations = new Queue<Quaternion>();

        for (int i = 0; i < 100; i++)
        {
            positions.Enqueue(new Vector3(Random.Range(-150, 150), 6, Random.Range(-150, 150)));
            rotations.Enqueue(Quaternion.identity);
        }

        Pooler.Instance.SpawnEntirePool("Bot", positions, rotations);
    }

    //void FixedUpdate()
    //{
    //    try
    //    {
    //        Pooler.Instance.SpawnPoolObject("Bot", new Vector3(Random.Range(-150, 150), 3, Random.Range(-150, 150)), Quaternion.identity);
    //    }
    //    catch (InvalidOperationException e)
    //    {
    //        Debug.Log("All objects from pool already spawned");
    //        Debug.Log(e);
    //    }
    //}
}
