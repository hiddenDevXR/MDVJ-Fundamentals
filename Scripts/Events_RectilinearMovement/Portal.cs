using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public Portal exitPortal;
    public GameObject spawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            other.transform.position = new Vector3(exitPortal.spawnPoint.transform.position.x,
                                                   other.transform.position.y,
                                                   exitPortal.spawnPoint.transform.position.z);
    }
}
