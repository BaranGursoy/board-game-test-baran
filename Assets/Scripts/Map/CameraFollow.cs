using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float smoothTime = 0.125f;
    [SerializeField] private float _cameraTurnSpeed = 6f;

    private Vector3 _velocity;
    private Vector3 offset;
    private float initialXRotation;

    void Start()
    {
        offset = transform.position - playerTransform.position;
        initialXRotation = transform.rotation.eulerAngles.x;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        desiredPosition.y = transform.position.y;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothTime);
        transform.position = smoothedPosition;

        Quaternion originalRotation = transform.rotation;
        transform.LookAt(playerTransform);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = originalRotation;
        
        Vector3 targetEulerAngles = targetRotation.eulerAngles;
        targetEulerAngles.x = initialXRotation;
        targetRotation = Quaternion.Euler(targetEulerAngles);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _cameraTurnSpeed * Time.deltaTime);
    }
}
