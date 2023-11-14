using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject vCam1, vCam2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            vCam1.SetActive(true);
            vCam2.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            vCam1.SetActive(false);
            vCam2.SetActive(true);
        }
    }
}
