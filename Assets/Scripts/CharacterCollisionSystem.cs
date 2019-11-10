using UnityEngine;

public class CharacterCollisionSystem : MonoBehaviour
{
    public bool groundFlag = false;
    public GameObject currentCollision;

 #region Getters

    public GameObject GetCurrentCollision()
    {
        return currentCollision;
    }

    public bool isGrounded()
    {
        return groundFlag;
    }

#endregion

    private void OnCollisionEnter(Collision collision)
    {
        currentCollision = collision.gameObject;

        if (collision.gameObject.tag == "Ground")
        {
            groundFlag = true;
        }
    }
}
