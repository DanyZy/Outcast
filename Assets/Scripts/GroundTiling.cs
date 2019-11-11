using UnityEngine;
using System.Collections.Generic;
using System;

public class GroundTiling : MonoBehaviour
{
    public CharacterCollisionSystem playerCCS;
    public GameObject platformPrefab;
    public Transform parentGround;

    public Vector3[] platformPositions = new Vector3[8];

    void Start()
    {
        playerCCS.onCollisionEnterFunction = CreateNewPlatform;
    }

    public void CreateNewPlatform(Collision _collision)
    {
        Transform currentCollision = _collision.gameObject.transform;

        Vector3 distanceFromCentreVector;
        Vector3 cathet = new Vector3(currentCollision.localScale.x, 0, 0);
        Vector3 hypotenuse = new Vector3(Mathf.Sqrt(2 * Mathf.Pow(currentCollision.localScale.x, 2)), 0, 0);

        for (int i = 0; i < platformPositions.Length; i++)
        {
            if (i % 2 == 0)
            {
                distanceFromCentreVector = cathet;
            }
            else
            {
                distanceFromCentreVector = hypotenuse;
            }

            platformPositions[i] = currentCollision.position + Quaternion.Euler(0, i * 45, 0) * distanceFromCentreVector;

            if (!Physics.CheckSphere(platformPositions[i], currentCollision.localScale.x * 0.01f))
            {
                Instantiate(platformPrefab, platformPositions[i], Quaternion.identity, parentGround);
            }
        }

        DeleteExistingPlatform(_collision);
    }

    public void DeleteExistingPlatform(Collision _collision)
    {
        GameObject currentCollision = _collision.gameObject;

        Transform[] platforms = new Transform[parentGround.childCount];
        for (int i = 0; i < parentGround.childCount; i++)
        {
            platforms[i] = parentGround.GetChild(i);
        }

        foreach (Transform platform in platforms)
        {
            if (Mathf.RoundToInt((platform.position - currentCollision.transform.position).magnitude) >
                Mathf.RoundToInt((new Vector3(Mathf.Sqrt(2 * Mathf.Pow(currentCollision.transform.localScale.x, 2)), 0, 0)).magnitude))
            {
                Destroy(platform.gameObject);
            }
        }
    }
}
