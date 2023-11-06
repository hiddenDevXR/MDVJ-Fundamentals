using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject box;

    void Start()
    {
        InvokeRepeating("DropObject", 2f, 2f);
    }

    private void DropObject()
    {
        Instantiate(box,transform.position, Quaternion.identity);
    }
}
