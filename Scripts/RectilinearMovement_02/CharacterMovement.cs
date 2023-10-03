using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform goal;
    public float speed = 1.0f;
    public float rotationSpeed = 2.0f;
    [Range(0.1f, 1f)]
    public float deadZone = 0.5f;

    public enum MovementType { LookAt, Slerp };
    public MovementType movementType = MovementType.LookAt;

    void Update()
    {
        if (movementType == MovementType.LookAt)
            ApplyLookAtMovement();

        else
            ApplySlerpMovement();
    }

    void ApplyLookAtMovement()
    {
        transform.LookAt(goal.position);

        Vector3 direction = goal.position - this.transform.position;

        float step = speed * Time.deltaTime;

        if (direction.magnitude > deadZone)
            this.transform.Translate(direction.normalized * step, Space.World);
    }

    void ApplySlerpMovement()
    {
        Vector3 direction = goal.position - this.transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        float step = speed * Time.deltaTime;

        if (direction.magnitude > deadZone)
            this.transform.Translate(0f, 0f, step);
    }
}
