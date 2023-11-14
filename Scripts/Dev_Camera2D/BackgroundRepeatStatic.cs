using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeatStatic : MonoBehaviour
{
    float background_w = 10.24f;

    void Update()
    {
        if (Camera.main.transform.position.x >= transform.position.x + background_w)
        {
            transform.position += new Vector3(background_w * 2f, 0f, 0f);
        }
    }
}
