using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_renderer;

    public Controller_2D robot;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        robot.OnAllItemsPicked += ChangeLayer;
    }


    void ChangeLayer()
    {
        m_renderer.color = new Color(1f, 1f, 1f, 0.5f);
        gameObject.layer = 9;
    }
}
