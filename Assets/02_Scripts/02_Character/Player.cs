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
    [HideInInspector] public float baseCriRate;
    [HideInInspector] public float baseCriDmgRate;
    [HideInInspector] public float finalDamageRate;
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
        /*var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                          x.TrainingType == TrainingType.AttSpeed);*/
        
        var attSpeedList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.AttSpeed);
        float additionalVal = 1;
        foreach (var UPPER in attSpeedList)
        {
            additionalVal += UPPER.AdditionalVal * PlayerManager.I.skillLevelDic[UPPER.TID];
        }
        print($"공격 속도: {baseSpeed * additionalVal}");
        return baseSpeed * additionalVal;
        
    }
    
    public float GetGrowthAttPower()
    {
        var normalAttPower = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.AttPower).OrderBy(x => x.IsRate);
        float finalPower = basePower;
        foreach (var VARIABLE in normalAttPower)
        {
            // Rate가 아닐 때
            if (VARIABLE.IsRate == 0)
            {
                finalPower += VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
            }
            // Rate일 때
            else
            {
                finalPower *= 1 + VARIABLE.AdditionalVal * PlayerManager.I.skillLevelDic[VARIABLE.TID];
            }
        }
        
        return finalPower;
    }
    
    public float GetGrowthCriRate()
    {
        var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                          x.TrainingType == TrainingType.CriRate);
        return baseCriRate + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID];
    }
    
    public float GetGrowthCriDamageRate()
    {
        var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                          x.TrainingType == TrainingType.CriDamageRate);
        return baseCriDmgRate + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID];
    }
    
    public float GetGrowthFinalDamageRate()
    {
        var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                          x.TrainingType == TrainingType.FinalDamageRate);
        return finalDamageRate + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID];
    }
}