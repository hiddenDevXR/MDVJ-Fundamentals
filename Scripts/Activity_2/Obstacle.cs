using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private MeshRenderer m_MeshRenderer;

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController == null) return;

            m_MeshRenderer.material.color = Color.red;
            playerController.score++;
            string message = gameObject.name + " obtained!" + " Your current score is: " + playerController.score.ToString();
            Debug.Log(message);
        }
            
    }
}
