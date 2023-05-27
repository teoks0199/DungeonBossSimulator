using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothing;
    public Vector3 offset;
    private GameObject playerModel;

    void Start()
    {
        playerModel = PlayerStats.playerStats.playerModel;
    }

    void Update() {
        playerModel = PlayerStats.playerStats.playerModel;
    }

void LateUpdate()
{
    if (playerModel != null)
    {
        Vector3 targetPosition = playerModel.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
    else
    {
        Debug.Log("Player model is null!");
    }
}

}

