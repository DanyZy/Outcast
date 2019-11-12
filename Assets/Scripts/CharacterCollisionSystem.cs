using UnityEngine;

public class CharacterCollisionSystem : MonoBehaviour
{
    bool groundFlag = false;

    public delegate void OnCollisionFunction(Collision collision);

    public OnCollisionFunction onCollisionEnterFunction;

 #region Getters

    public bool isGrounded()
    {
        return groundFlag;
    }

#endregion

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnterFunction(collision);

        if (collision.gameObject.tag == "Ground")
        {
            groundFlag = true;
        } 
    }
}
