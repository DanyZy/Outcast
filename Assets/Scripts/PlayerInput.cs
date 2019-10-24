using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    [Space]
    [Header("Ragdoll Movement Toggle")]
    public bool isSmooth = false;

    [Space]
    [Header("Run Button")]
    public KeyCode acceleration = KeyCode.LeftShift;

    Vector2 input = Vector2.zero;

    CharacterMovement cm;

    private void Start()
    {
        cm = GetComponent<CharacterMovement>();
    }

    void FixedUpdate()
    {

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        cm.Move(input, Input.GetKey(acceleration), isSmooth);
    }
}
