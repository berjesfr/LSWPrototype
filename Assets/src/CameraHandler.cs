using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public Transform playerTransform;
    public float chaseBehindSpeed = 1.0f;


    private void Update()
    {
        Vector3 cameraToPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraToPosition, chaseBehindSpeed * Time.deltaTime);
    }

}
