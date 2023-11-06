using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float range = 3f;
    Vector2 initialPosition;
    Rigidbody2D m_rigibody;

    void Start()
    {
        initialPosition = transform.position;
        m_rigibody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(initialPosition.x, initialPosition.y + Mathf.Sin(Time.fixedTime) * range);

        m_rigibody.position = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform otherTransform = collision.transform;
            otherTransform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform otherTransform = collision.transform;
            otherTransform.parent = null;
        }
    }
}
