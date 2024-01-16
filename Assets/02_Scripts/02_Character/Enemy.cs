using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 500;

    public void GetDamage(AttackInfo attackInfo)
    {
        float damage = attackInfo.attPower;
        float criRate = attackInfo.criRate * 1000;
  
        // 크리티컬 확률 계산
        if (Random.Range(0, 1000) <= criRate)
        {
            // 크리티컬 발동시 데미지 증가
            damage = damage + damage * attackInfo.criDamageRate;
        }
        
        // 최종 데미지 증가
        damage = damage + damage * attackInfo.finalDamageRate;
        //Debug.Log($"최종 데미지 증가: {attackInfo.finalDamageRate}");
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
        Destroy(transform.parent.gameObject);
        SpawnManager.I.DecreaseCount();

        int gold = 0;
        int stone = 0;
        // 테이블을 참조하여 현재 스테이지에 해당하는 골드와, 스톤 값을 가지고 온다.
        foreach (var VARIABLE in DefaultTable.StageMonster.GetList())
        {
            gold = Mathf.RoundToInt(VARIABLE.Gold_Base * (1 + VARIABLE.Gold_Increase * StageManager.I.currentStage));
            stone = Mathf.RoundToInt(VARIABLE.Stone_Base * (1 + VARIABLE.Stone_Increase * StageManager.I.currentStage));
        }
        
        // player의 PlayerManager의 gold와 stone값을 증가시킨다.
        GoodsManager.I.IncreaseGold(gold);
        GoodsManager.I.IncreaseStone(stone);

    }
}