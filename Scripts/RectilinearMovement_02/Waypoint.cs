using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private MeshRenderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    public void SetAsCurrent()
    {
        m_Renderer.material.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            m_Renderer.material.color = Color.yellow;
    }
}
