using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{  
    public float speed = 2f;
    bool escapeEnable =  false;

    void Start()
    {
        GameManager._player.GetComponent<PlayerInteractions>().OnItemTouch += BeginEscape;
    }

    private void Update()
    {
        if(escapeEnable)
            Escape();
    }

    public void Escape()
    {
        Vector3 direction = GameManager.GetPlayerPosition() - this.transform.position;
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(GameManager.GetPlayerPosition(), transform.position) < 2f)
            this.transform.Translate(-direction.normalized * step, Space.World);
    }

    public void BeginEscape()
    {
        escapeEnable = true;
    }
}
