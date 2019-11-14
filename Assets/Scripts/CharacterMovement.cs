using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterCollisionSystem))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    private CharacterCollisionSystem ccs;

    [Header("Movement Speed")]
    public float walkSpeed = 10f;
    public float runSpeed = 20f;

    [Header("Movement & Rotation Smoothing")]
    public float smoothTime = 0.1f;

    private new Transform camera;

    private Vector3 moveDirection = Vector3.zero;

    private float currentVelocityRotate;
    private float currentVelocitySpeed;
    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ccs = GetComponent<CharacterCollisionSystem>();
        camera = Camera.main.transform;
    }

    public void Move(Vector2 _input, bool _isAcceleration, bool isSmoothly = false)
    {
        if (_input != Vector2.zero)
        {
            //Character rotation calculation
            float targetRotation = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocityRotate, smoothTime);

            //Character speed calculation
            float targetSpeed = _isAcceleration ? runSpeed : walkSpeed;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocitySpeed, smoothTime);

            //Movement type selection
            if (ccs.isGrounded())
            {
                if (isSmoothly)
                {
                    //By physics
                    rb.AddForce(transform.forward * currentSpeed);
                }
                else
                {
                    //By position calculation
                    transform.position += transform.forward * currentSpeed * Time.deltaTime;
                }
            }
        }
    }
}
