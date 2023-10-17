using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealMovement : MonoBehaviour
{

    public Transform goal;
    public float speed = 1.0f;

    bool enable = false;

    // Update is called once per frame
    void Update()
    {
        if (!enable) return; 

        this.transform.LookAt(goal.position);

        Vector3 direction = goal.position - this.transform.position;

        float step = speed * Time.deltaTime;
        this.transform.Translate(direction.normalized * step, Space.World);
        Debug.DrawRay(this.transform.position, direction, Color.red);
    }

    public void EnableMovement()
    {
        enable = true;
    }
}
