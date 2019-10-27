using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [Space]
    public Transform target;
    public Vector3 offset;

    [Header("Rotate")]
    public float speedRotate = 10;
    public float minY;
    public float maxY;

    [Header("Zoom")]
    public float speedZoom = 20;
    public float minZoom = 1f;
    public float maxZoom = 8f;
    private float distanceFromTarget = 5;

    [Header("Camera Smoothing")]
    public float rotationSmoothTime = 0.1f;
    public float movementSmoothTime = 0.1f;

    [Space]
    public bool isFixed;

    private float inputX;
    private float inputY;

    private Vector3 currentRotation;
    private Vector3 currentPosition;
    private Vector3 currentVelocityAng;

    private Vector3 currentVelocityPos = Vector3.zero;

    private void FixedUpdate()
    {
        if (!isFixed)
        {
            //Rotation input
            inputX += Input.GetAxis("Mouse X") * speedRotate;
            inputY -= Input.GetAxis("Mouse Y") * speedRotate;
            inputY = Mathf.Clamp(inputY, minY, maxY);

            //Rotation around targent object calculation
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(inputY, inputX), ref currentVelocityAng, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
        }

        //Zoom calculation
        distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel") * speedZoom * Time.deltaTime;
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, minZoom, maxZoom);

        //Following targent object calculation
        currentPosition = Vector3.SmoothDamp(transform.position, (target.position + offset) - transform.forward * distanceFromTarget, ref currentVelocityPos, movementSmoothTime);
        transform.position = currentPosition;
    }
}
