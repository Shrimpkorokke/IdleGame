using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int fiveAttack = 0;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Enemy"))
        {
            var info = new AttackInfo();
            info.attPower = PlayerManager.I.player.GetGrowthAttPower();
            info.criRate = PlayerManager.I.player.GetGrowthCriRate();
            info.criDamageRate = PlayerManager.I.player.GetGrowthCriDamageRate();
            info.finalDamageRate = PlayerManager.I.player.GetGrowthFinalDamageRate();
            info.normalAddDmg = PlayerManager.I.abilitySkillLevelDic[Const.NORMAL_ADD_DMG] > 0;
            info.bossAddDmg = PlayerManager.I.abilitySkillLevelDic[Const.BOSS_ADD_DMG] > 0;
            info.excution = PlayerManager.I.abilitySkillLevelDic[Const.EXECUTION] > 0;
            
            if (PlayerManager.I.abilitySkillLevelDic[Const.FIVE_ATTACK] > 0)
            {
                if (fiveAttack >= 4)
                {
                    fiveAttack = 0;
                    info.fiveAttack = true;
                }
                else
                {
                    fiveAttack++;
                    info.fiveAttack = false;
                }
            }
            
            collider2D.gameObject.GetComponent<Enemy>()?.GetDamage(info);
            ObjectPoolManager.I.SpawnFromPool("HitEffect", collider2D.transform.position, quaternion.identity);
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
    // 일반몹 추댐 특성
    public bool normalAddDmg;
    // 보스몹 추댐 특성
    public bool bossAddDmg;
    // 5타 공격
    public bool fiveAttack;
    // 처형
    public bool excution;
}
