using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotSpawner : MonoBehaviour
{
    private void Start()
    {
        //Pooler.Instance.SpawnPoolObject("HexCells", Vector3.zero, Quaternion.identity);

        Queue<Vector3> positions = new Queue<Vector3>();
        Queue<Quaternion> rotations = new Queue<Quaternion>();

        for (int i = 0; i < 100; i++)
        {
            positions.Enqueue(new Vector3(Random.Range(-150, 150), 6, Random.Range(-150, 150)));
            rotations.Enqueue(Quaternion.identity);
        }

        Pooler.Instance.SpawnEntirePool("Bots", positions, rotations);
    }
}
