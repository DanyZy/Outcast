using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement Speed")]
    public float walkSpeed = 10f;
    public float runSpeed = 20f;

    [Space]
    [Header("Movement & Rotation Smoothing")]
    public float smoothTime = 0.1f;

    private new Transform camera;

    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = false;

    private float currentVelocityRotate;
    private float currentVelocitySpeed;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            float targetSpeed = (_isAcceleration ? runSpeed : walkSpeed);
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocitySpeed, smoothTime);

            //Movement type selection
            if (isGrounded)
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

#region GroundCheck

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }

#endregion
}
