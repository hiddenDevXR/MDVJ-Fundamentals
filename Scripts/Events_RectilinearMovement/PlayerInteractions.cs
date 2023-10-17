using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    public delegate void Message();
    public event Message OnItemTouch;
    public event Message OnEnterZone;
    public event Message OnExitZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Repelent"))
        {
            Destroy(other.gameObject);
            OnItemTouch();
        }

        if (other.CompareTag("MagicZone"))
        {
            OnEnterZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MagicZone"))
        {
            OnExitZone();
        }
    }
}
