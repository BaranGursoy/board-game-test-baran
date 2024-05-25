using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float screenEdgeBuffer = 0.1f;
    [SerializeField] private float smoothSpeed = 0.125f;
    
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 playerViewportPosition = Camera.main.WorldToViewportPoint(player.position);

            float offsetX = 0f;

            if (playerViewportPosition.x < screenEdgeBuffer)
            {
                offsetX = playerViewportPosition.x - screenEdgeBuffer;
            }
            else if (playerViewportPosition.x > 1 - screenEdgeBuffer)
            {
                offsetX = playerViewportPosition.x - (1 - screenEdgeBuffer);
            }

            Vector3 desiredPosition = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);
            
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}