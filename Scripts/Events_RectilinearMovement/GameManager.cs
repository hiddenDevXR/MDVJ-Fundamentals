using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameObject _player;

    private void Awake()
    {
        _player = player;
    }

   public static Vector3 GetPlayerPosition()
   {
        return new Vector3(_player.transform.position.x, 1f, _player.transform.position.z);
   }
}
