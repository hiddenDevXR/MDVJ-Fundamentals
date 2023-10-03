using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public List<GameObject> waypoints;
    private GameObject currentTarget;
    private int targetIndex = 0;

    public float speed = 1.0f;
    public float rotationSpeed = 2.0f;
    [Range(0.1f, 1f)]
    public float deadZone = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        currentTarget = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = currentTarget.transform.position - this.transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        float step = speed * Time.deltaTime;

        if (direction.magnitude > deadZone)
            this.transform.Translate(0f, 0f, step);

        else
            SetNewTarget();
    }

    void SetNewTarget()
    {
        targetIndex++;

        if(targetIndex >= waypoints.Count)
            targetIndex = 0;

        currentTarget = waypoints[targetIndex];

        Waypoint waypoint = currentTarget.GetComponent<Waypoint>();
        if(waypoint != null) 
            waypoint.SetAsCurrent();
    }
}
