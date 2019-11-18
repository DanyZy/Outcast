using UnityEngine;

public class GroundTiling : MonoBehaviour
{
    [Header("Initional Platform Position")]
    public Vector3 startPosition;

    [Header("Character Collision Manager")]
    public CharacterCollisionSystem playerCCS;

    [Header("Ground Setup")]
    public GameObject platformPrefab;
    public Transform parentGround;

    private Vector3[] platformPositions = new Vector3[6];

    private void Start()
    {
        //Initional platform
        Instantiate(platformPrefab, startPosition, Quaternion.identity, parentGround);

        Instantiate(platformPrefab, startPosition + new Vector3(100, 0, 0), Quaternion.identity, parentGround);


        playerCCS.onCollisionEnterFunction = CreateNewPlatform;
    }

    private void CreateNewPlatform(Collision _collision)
    {
        Transform currentCollision = _collision.gameObject.transform;

        Vector3 distanceVector = new Vector3(HexMetrics.innerRadius * 2f, 0, 0);

        for (int i = 0; i < platformPositions.Length; i++)
        {
            platformPositions[i] = currentCollision.position + Quaternion.Euler(0, i * 60, 0) * distanceVector;

            //Someday I'll regret for this crutch
            if (!Physics.CheckSphere(platformPositions[i], currentCollision.localScale.x * 0.01f))
            {
                Instantiate(platformPrefab, platformPositions[i], Quaternion.identity, parentGround);
            }
        }

        DeleteExistingPlatform(_collision);
    }

    private void DeleteExistingPlatform(Collision _collision)
    {
        Transform currentCollision = _collision.gameObject.transform;

        Transform[] platforms = new Transform[parentGround.childCount];
        for (int i = 0; i < parentGround.childCount; i++)
        {
            platforms[i] = parentGround.GetChild(i);
        }

        foreach (Transform platform in platforms)
        {
            float distanceToPlatform = Mathf.RoundToInt((platform.position - currentCollision.position).magnitude);
            float desiredDistanceToPlatform = Mathf.RoundToInt(new Vector3(HexMetrics.innerRadius * 2f, 0, 0).magnitude);

            if (distanceToPlatform > desiredDistanceToPlatform)
            {
                Destroy(platform.gameObject);
            }
        }
    }
}
