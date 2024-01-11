using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Enemy"))
        {
            collider2D.gameObject.GetComponent<Enemy>()?.GetDamage(PlayerManager.I.player.GetGrowthAttPower());
        }
    }   
}
