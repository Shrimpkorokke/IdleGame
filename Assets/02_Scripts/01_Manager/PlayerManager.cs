using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public GameObject player;
    private void Awake()
    {
        if(player == null)
            player = GameObject.Find("Player");
    }
}
