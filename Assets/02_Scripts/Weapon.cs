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
            var info = new AttackInfo();
            info.attPower = PlayerManager.I.player.GetGrowthAttPower();
            info.criRate = PlayerManager.I.player.GetGrowthCriRate();
            info.criDamageRate = PlayerManager.I.player.GetGrowthCriDamageRate();
            info.finalDamageRate = PlayerManager.I.player.GetGrowthFinalDamageRate();
            
            collider2D.gameObject.GetComponent<Enemy>()?.GetDamage(info);
        }
    }   
}

public struct AttackInfo
{
    // 공격력
    public float attPower;
    // 치명타 확률
    public float criRate;
    // 치명타 대미지
    public float criDamageRate;
    // 최종 대미지
    public float finalDamageRate;
}
