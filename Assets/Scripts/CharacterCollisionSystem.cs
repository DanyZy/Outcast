using UnityEngine;

public class CharacterCollisionSystem : MonoBehaviour
{
    public int groundCollisionCounter = 0;

    public delegate void OnCollisionFunction(Collision collision);

    public OnCollisionFunction onCollisionEnterFunction;

    #region Getters

    public bool isGrounded()
    {
        return groundCollisionCounter != 0;
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnterFunction(collision);

        if (collision.gameObject.tag == "Ground")
        {
            groundCollisionCounter++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundCollisionCounter--;
        }
    }
}
