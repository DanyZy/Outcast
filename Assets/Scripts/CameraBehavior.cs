using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [Space]
    public Transform target;
    public Vector3 offset;

    [Header("Zoom")]
    public float speedZoom = 50;
    public float minZoom = 5f;
    public float maxZoom = 25f;
    private float distanceFromTarget = 10f;

    [Header("Camera Smoothing")]
    public float movementSmoothTime = 0.1f;

    private Vector3 currentPosition;

    private Vector3 currentVelocityPos = Vector3.zero;

    private void FixedUpdate()
    {
        //Zoom calculation
        distanceFromTarget -= Input.GetAxis("Mouse ScrollWheel") * speedZoom * Time.deltaTime;
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, minZoom, maxZoom);

        //Following targent object calculation
        currentPosition = Vector3.Lerp(transform.position, (target.position + offset) - transform.forward * distanceFromTarget, movementSmoothTime);
        transform.position = currentPosition;
    }
}
