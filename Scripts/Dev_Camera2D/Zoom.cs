using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    
    void Update()
    {
        float zoom = Input.GetAxis("Vertical") * Time.deltaTime * 5f;
        virtualCamera.m_Lens.OrthographicSize += zoom;
    }
}
