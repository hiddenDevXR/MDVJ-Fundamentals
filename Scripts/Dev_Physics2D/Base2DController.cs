using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base2DController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;

    public float speed = 3f;

    public TMP_Text UILog;

    void Start()
    {
        if (m_rigidbody == null)
            m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float step = Time.deltaTime * speed;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * step, m_rigidbody.velocity.y, 0f);
        m_rigidbody.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PhysicsObstacle obstacle = collision.gameObject.GetComponent<PhysicsObstacle>();

        if (obstacle != null)
        {
            UILog.text = "Debug Log: " + "I am colliding with " + "'" + obstacle.GetName() + "'" +
               " with " + obstacle.GetRigidbody2D().mass.ToString() + " mass.";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhysicsObstacle obstacle = collision.gameObject.GetComponent<PhysicsObstacle>();

        if (obstacle != null)
        {
            UILog.text = "Debug Log: " + "I am overlaping a Trigger named " + "'" + obstacle.GetName() + "'.";
        }
    }
}
