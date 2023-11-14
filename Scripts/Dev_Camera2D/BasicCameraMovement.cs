using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraMovement : MonoBehaviour
{
    void Update()
    {
        float step = Time.deltaTime * 5f;
        float horizontal = Input.GetAxis("Horizontal") * step;

        transform.position += Vector3.right * horizontal;
    }
}
