using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    enum EnemyState { Idle, Attack, Dead };
    EnemyState currentState = EnemyState.Idle;


    void Start()
    {
        if (m_animator == null)
            m_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_animator.SetInteger("State", (int)EnemyState.Attack);
            Controller_2D playerController = other.GetComponent<Controller_2D>();
            playerController.TakeDamage();
        }
            
    }
}
