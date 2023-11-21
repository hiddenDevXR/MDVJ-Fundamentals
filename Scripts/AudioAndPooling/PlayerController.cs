using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float hitVol = 1f;
    AudioSource m_audioSource;
    public AudioClip m_audioClip;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            hitVol = speed * 0.1f;
            m_audioSource.PlayOneShot(m_audioClip, hitVol);
        }
    }
}
