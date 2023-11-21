using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    AudioSource m_AudioSource;
    public float speed = 3.0f;
    bool m_Paused = true;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float step = Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.P)) {
            m_Paused = false;
            m_AudioSource.Play();
        }

        else if(Input.GetKeyDown(KeyCode.S)) {
            m_Paused = true;
            m_AudioSource.Stop();
        }

        if (!m_Paused)
        {
            m_AudioSource.pitch = Random.Range(1f, 3f);
            transform.position += Vector3.right * step;
        }        
    }
}
