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
        //Debug.Log($"크리티컬 확률: {criRate}");
        //Debug.Log($"크리티컬 데미지 증가: {1 + aaa.criDamageRate}");
        // 크리티컬 확률 계산
        /*if (Random.Range(0, 100) <= criRate)
        {
            // 크리티컬 발동시 데미지 증가
            damage = damage * (1 + aaa.criDamageRate);
        }*/
        
        // 최종 데미지 증가
        damage = damage * (1 + aaa.finalDamageRate);
        //Debug.Log($"최종 데미지 증가: {1 + aaa.finalDamageRate}");
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