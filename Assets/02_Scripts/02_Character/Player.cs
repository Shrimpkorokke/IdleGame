using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField, GetComponentInChildren] private Weapon weapon;
    [SerializeField, GetComponentInChildren] private Animator ani;
    
    public int basePower;
    public float baseSpeed;
    public float baseCriRate;
    public float baseCriDmgRate;
    public float baseFinalDamageRate;
    void Start()
    {
        StartCoroutine(Attack());
        DefaultTable.Training.GetList();
    }

    private void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            AttackAnim();
            yield return null;
        }
    }

    public void AttackAnim()
    {
        //ani.Play("Pickaxe_Attack");
        //ani.SetTrigger("onAttack");
        ani.SetBool("isAttack", PlayerManager.I.isAttack);
    }
    
    public void SetAttackSpeed()
    {
        print("SetAttackSpeed");
        ani.SetFloat("attackSpeed", GetGrowthAttSpeed());
    }
    
      
    public float GetGrowthAttSpeed()
    {
        var attSpeedList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.AttSpeed);
        float additionalVal = 0;
        foreach (var VARIABLE in attSpeedList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
        }
        //print($"공격 속도: {baseSpeed * additionalVal}");
        return baseSpeed * 1 + additionalVal;
        
    }
    
    public float GetGrowthAttPower()
    {
        var attPowerList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.AttPower).OrderBy(x => x.IsRate);
        float finalPower = basePower;
        foreach (var VARIABLE in attPowerList)
        {
            // Rate가 아닐 때
            if (VARIABLE.IsRate == 0)
            {
                finalPower += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
            }
            // Rate일 때
            else
            {
                finalPower += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
            }
        }
        
        return finalPower;
    }
    
    public float GetGrowthCriRate()
    {
        var criRateList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.CriRate);
        float additionalVal = 0;
        foreach (var VARIABLE in criRateList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
        }
        return baseCriRate * 1 + additionalVal;
    }
    
    public float GetGrowthCriDamageRate()
    {
        var criDmgRateList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.CriDamageRate);
        float additionalVal = 0;
        foreach (var VARIABLE in criDmgRateList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
        }
        return baseCriDmgRate * 1 + additionalVal;
    }
    
    public float GetGrowthFinalDamageRate()
    {
        var finalDmgRateList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.FinalDamageRate);
        float additionalVal = 0;
        foreach (var VARIABLE in finalDmgRateList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
        }
        return baseFinalDamageRate + additionalVal;
    }
}