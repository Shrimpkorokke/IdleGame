using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Unified hp = new Unified(500);
    [SerializeField] private bool isBoss;

    public void Start()
    {
        foreach (var stageMonster in DefaultTable.StageMonster.GetList())
        {
            BigInteger temp = (BigInteger)(stageMonster.HP_Base *
                                         Mathf.Pow(StageManager.I.GetCurrnetStage(), isBoss ? stageMonster.BossHP_Increase : stageMonster.HP_Increase));
            
            hp = new Unified(temp);
            Debug.Log($"hp: {hp} HP_Increase: {stageMonster.HP_Increase}");
        }
    }

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
        
        StageManager.I.IncreaseCount(2);
        
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
        foreach (var stageMonster in DefaultTable.StageMonster.GetList())
        {
            gold = (BigInteger)(stageMonster.Gold_Base *
                                Mathf.Pow(StageManager.I.GetCurrnetStage(), stageMonster.Gold_Increase));
            stone = (BigInteger)(stageMonster.Stone_Base *
                                 Mathf.Pow(StageManager.I.GetCurrnetStage(), stageMonster.Stone_Increase));

            if (isBoss)
            {
                gold *= 2;
                stone *= 2;
            }
            
            Debug.Log($"gold: {gold}. stone: {stone} \n stageMonster.Gold_Increase {stageMonster.Gold_Increase}, stageMonster.Stone_Increase: {stageMonster.Stone_Increase}");
        }
        
        // player의 PlayerManager의 gold와 stone값을 증가시킨다.
        GoodsManager.I.IncreaseGold(new Unified(gold));
        GoodsManager.I.IncreaseStone(new Unified(stone));
        
        SpawnManager.I.RemoveEnemy(transform.parent.gameObject);
        
        if (isBoss == true)
        {
            SpawnManager.I.DieBoss();
            StageManager.I.DieBoss();
        }
        else
        {
            StageManager.I.IncreaseCount(5);
        }
        
        Destroy(transform.parent.gameObject);
    }
}