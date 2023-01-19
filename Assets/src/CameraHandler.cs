using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public Transform m_PlayerTransform;
    public float m_ChaseBehindSpeed = 1.0f;


    private void Update()
    {
        Vector3 cameraToPosition = new Vector3(m_PlayerTransform.position.x, m_PlayerTransform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraToPosition, m_ChaseBehindSpeed * Time.deltaTime);
    }

}
