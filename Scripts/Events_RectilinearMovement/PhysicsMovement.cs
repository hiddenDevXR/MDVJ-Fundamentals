using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhysicsMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float speed = 5f;
    public TMP_Text scoreText;
    public TMP_Text speedText;
    public TMP_Text message;
    private int score = 0;



    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        speedText.text = speed.ToString();
        scoreText.text = score.ToString();
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float step = Time.deltaTime * speed;

        m_Rigidbody.MovePosition(transform.position + direction * Time.deltaTime * step);
    }

    public void SetSpeedTo(int targetSpeed)
    {
        speed = targetSpeed;
        speedText.text = speed.ToString();
    }

    private void CollectItem()
    {
        score++;
        scoreText.text = score.ToString();
        message.text = "Point collected!";
    }

    private void TakeDamage()
    {
        SetSpeedTo(100);
        message.text = "You took damage. Speed is affected!";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            CollectItem();
            Destroy(other.gameObject);
        }
            

        else if (other.CompareTag("Hazard"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
            
    }
}
