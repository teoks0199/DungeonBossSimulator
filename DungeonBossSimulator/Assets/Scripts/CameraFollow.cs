using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothing;
    public Vector3 offset;
    private GameObject playerModel;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private void Start()
    {
        playerModel = PlayerStats.playerStats.playerModel;
    }

    private void Update()
    {
        playerModel = PlayerStats.playerStats.playerModel;
    }

    private void LateUpdate()
    {
        if (playerModel != null)
        {
            Vector3 targetPosition = playerModel.transform.position + offset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
        else
        {
            Debug.Log("Player model is null!");
        }
    }
}

