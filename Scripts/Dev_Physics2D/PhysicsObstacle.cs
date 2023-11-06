using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObstacle : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public string GetName()
    {
        return gameObject.name;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }

    
}
