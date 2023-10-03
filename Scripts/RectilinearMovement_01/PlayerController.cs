using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1f;
    public int score = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed += 1;
            Debug.Log("By pressing space you increase the velocity to: " + speed.ToString());
        }

        float step = Time.deltaTime * speed;
        float horizontal = Input.GetAxis("Horizontal") * step;
        float vertical = Input.GetAxis("Vertical") * step;

        Vector3 composeVector = new Vector3(horizontal, 0f, vertical);

        transform.position += composeVector;
    }

    
}
