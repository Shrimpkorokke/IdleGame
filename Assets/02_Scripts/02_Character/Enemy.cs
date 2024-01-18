using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Unified hp = new Unified(500);
    [SerializeField] private bool isBoss;
    public void GetDamage(AttackInfo attackInfo)
    {
        Unified damage = new Unified(attackInfo.attPower);
        float criRate = attackInfo.criRate * 1000;
        Unified criDamageRate = new Unified(attackInfo.criDamageRate);
        Unified finalDamageRate = new Unified(attackInfo.finalDamageRate);
            
        // 크리티컬 확률 계산
        if (Random.Range(0, 1000) <= criRate)
        {
            // 크리티컬 발동시 데미지 증가
            damage = damage + damage * criDamageRate;
        }
        
        // 최종 데미지 증가
        damage = damage + damage * finalDamageRate;
        FloatingTextController.I.CreateFloatingText(damage.IntPart.BigintToString(), transform);
        
        // 체력 감소
        hp -= damage;

        if (hp.IntPart <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        BigInteger gold = 0;
        BigInteger stone = 0;
        // 테이블을 참조하여 현재 스테이지에 해당하는 골드와, 스톤 값을 가지고 온다.
        foreach (var VARIABLE in DefaultTable.StageMonster.GetList())
        {
            gold = Mathf.RoundToInt(VARIABLE.Gold_Base * (1 + VARIABLE.Gold_Increase * StageManager.I.currentStage));
            stone = Mathf.RoundToInt(VARIABLE.Stone_Base * (1 + VARIABLE.Stone_Increase * StageManager.I.currentStage));
        }
        
        // player의 PlayerManager의 gold와 stone값을 증가시킨다.
        GoodsManager.I.IncreaseGold(new Unified(gold));
        GoodsManager.I.IncreaseStone(new Unified(stone));
        
        SpawnManager.I.RemoveEnemy(transform.parent.gameObject);

        if (isBoss == true)
        {
            SpawnManager.I.bossSpawned = false;
        }
        
        Destroy(transform.parent.gameObject);
    }
}