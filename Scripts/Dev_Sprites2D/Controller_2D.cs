using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_2D : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;
    [SerializeField]
    private Animator m_animator;
    
    private SpriteRenderer m_spriteRenderer;

    private bool grounded = true;
    public float speed = 3f;
    public float jumpForcer = 10f;

    enum PlayerState { Idle, Run, Jump, Dead };
    PlayerState currentState = PlayerState.Idle;

    void Start()
    {
        if(m_rigidbody == null)
            m_rigidbody = GetComponent<Rigidbody2D>();
        if(m_animator == null)
            m_animator = GetComponent<Animator>();

        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbody.AddForce(new Vector2(m_rigidbody.velocity.x, jumpForcer));
            SetAnimation(PlayerState.Jump);
            grounded = false;
        }
    }

    private void FixedUpdate()
    {
        float step = Time.deltaTime * speed;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal") * step, m_rigidbody.velocity.y, 0f);
        
        if (direction.x != 0f)
        {
            if(direction.x < 0f)
                m_spriteRenderer.flipX = true;
            else if(direction.x > 0f)
                m_spriteRenderer.flipX = false;

            if (grounded)
                SetAnimation(PlayerState.Run);

            else
                SetAnimation(PlayerState.Jump);
        }

        if (direction.x == 0f && grounded)
            SetAnimation(PlayerState.Idle);

        m_rigidbody.velocity = direction;
    }

    private void SetAnimation(PlayerState state)
    {
        m_animator.SetInteger("State", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    public void TakeDamage()
    {
        SetAnimation(PlayerState.Dead);
    }
}
