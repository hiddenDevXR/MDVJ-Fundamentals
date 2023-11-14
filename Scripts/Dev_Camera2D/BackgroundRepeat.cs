using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    public Transform nextImagePoint;
    public float leftLimit;
    public float speedOffset = 5.0f;

    void Update()
    {
        float step = Time.deltaTime * speedOffset;
        float horizontal = Input.GetAxis("Horizontal") * step;

        transform.position = new Vector3(transform.position.x + horizontal, 0, transform.position.z);

        if(transform.position.x <= leftLimit)
        {
            transform.position = new Vector3(nextImagePoint.position.x, 0f, transform.position.z);
        }
    }
}
