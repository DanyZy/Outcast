using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotSpawner : MonoBehaviour
{
    void FixedUpdate()
    {
        try
        {
            Pooler.Instance.SpawnPoolObject("Bot", new Vector3(Random.Range(-150, 150), 3, Random.Range(-150, 150)), Quaternion.identity);
        }
        catch (InvalidOperationException e)
        {
            Debug.Log("All objects from pool already spawned");
            Debug.Log(e);
        }
    }
}
