using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 500;

    public void GetDamage(AAA aaa)
    {
        float damage = aaa.attPower;
        float criRate = aaa.criRate * 100;
        // 크리티컬 확률 계산
        if (Random.Range(0, 100) <= criRate)
        {
            // 크리티컬 발동시 데미지 증가
            damage *= aaa.criDamageRate;
        }
        
        // 최종 데미지 증가
        damage *= aaa.finalDamageRate;
        
        FloatingTextController.I.CreateFloatingText(damage.ToString(), transform);
        
        // 체력 감소
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //print("Die");
    }
}