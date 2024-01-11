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
        var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                          x.TrainingType == TrainingType.AttSpeed);
        print($"111111 {a.AdditionalVal}");
        print($"GetGrowthAttSpeed {1 + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID]}");
        return 1 + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID];
    }
    
    public float GetGrowthAttPower()
    {
        var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                          x.TrainingType == TrainingType.AttPower);
        print($"111111 {a.AdditionalVal}");
        print($"GetGrowthAttPower {basePower + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID]}");
        return basePower + a.AdditionalVal * PlayerManager.I.skillLevelDic[a.TID];
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