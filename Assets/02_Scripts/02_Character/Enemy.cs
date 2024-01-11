using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BigInteger hp = 500;
    
    public void GetDamage(BigInteger damage)
    {
        FloatingTextController.I.CreateFloatingText(damage.ToString(), transform);
        hp -= damage;
        //print($"GetDamage hp: {hp}, damage: {damage}");

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