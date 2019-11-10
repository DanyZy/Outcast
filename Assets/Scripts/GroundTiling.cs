using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTiling : MonoBehaviour
{
    public GameObject player;
    public GameObject platformPrefab;
    public Transform parentGround;
    public Vector3[] platformPositions;

    private GameObject currentPlatform;

    void Update()
    {
        CreateNewPlatform();
    }

    void CreateNewPlatform()
    {
        GameObject currentCollision = player.GetComponent<CharacterCollisionSystem>().GetCurrentCollision();

        if (currentCollision)
        {
            Vector3 distanceFromCentreVector;
            Vector3 cathet = new Vector3(currentCollision.transform.localScale.x, 0, 0);
            Vector3 hypotenuse = new Vector3(Mathf.Sqrt(2 * Mathf.Pow(currentCollision.transform.localScale.x, 2)), 0, 0);

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

                platformPositions[i] = currentCollision.transform.position + Quaternion.Euler(0, i * 45, 0) * distanceFromCentreVector;

                if (!Physics.CheckSphere(platformPositions[i], 1f))
                {
                    Instantiate(platformPrefab, platformPositions[i], Quaternion.identity, parentGround);
                }
            }
        }
    }
}
