using UnityEngine;

public class CharacterCollisionSystem : MonoBehaviour
{
    private int groundCollisionCounter = 0;

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
        if (collision.gameObject.tag == "Ground")
        {
            //onCollisionEnterFunction(collision);

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
