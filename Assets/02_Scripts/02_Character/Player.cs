using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    [SerializeField, GetComponentInChildren] private Weapon weapon;
    [SerializeField, GetComponent] private Animator playerAni;
    [SerializeField, GetComponentInChildrenName("Pickaxe")] private Animator pickaxeAni;
    
    public int basePower;
    public float baseSpeed;
    public float baseCriRate;
    public float baseCriDmgRate;
    public float baseFinalDamageRate;
    void Start()
    {
        //StartCoroutine(Attack());
    }

    private void Update()
    {
        PickaxeAnim();
        PlayerAnim();
    }

    IEnumerator Attack()
    {
        while (true)
        {
            PickaxeAnim();
            yield return null;
        }
    }
    
    public void PlayerAnim()
    {
        playerAni.SetBool("isAttack", PlayerManager.I.isAttack);
    }

    public void PickaxeAnim()
    {
        //ani.Play("Pickaxe_Attack");
        //ani.SetTrigger("onAttack");
        pickaxeAni.SetBool("isAttack", PlayerManager.I.isAttack);
    }

    
    public void SetAttackSpeed()
    {
        print("SetAttackSpeed");
        pickaxeAni.SetFloat("attackSpeed", GetGrowthAttSpeed());
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