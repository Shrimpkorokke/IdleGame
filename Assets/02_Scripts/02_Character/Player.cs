using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    [SerializeField, GetComponentInChildren] public Weapon weapon;
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
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.trainingSkillLevelDic[VARIABLE.TID];
        }
        //print($"공격 속도: {baseSpeed * additionalVal}");
        return baseSpeed * 1 + additionalVal;
        
    }
    
    public float GetGrowthAttPower()
    {
        var attPowerTrainingList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.AttPower);
        var attPowerAbilityList = DefaultTable.Ability.GetList().FindAll(x => x.AbilityType == AbilityType.AttPower);
        var weaponAttPower = DefaultTable.Weapons.GetList().Find(x => x.TID == DataManager.I.playerData.currentWeapon).Attack_Power;
        
        float finalPower = basePower + weaponAttPower;
        float rate = 0;
        
        foreach (var VARIABLE in attPowerTrainingList)
        {
            // Rate가 아닐 때
            if (VARIABLE.IsRate == 0)
            {
                finalPower += VARIABLE.AdditionalVal * PlayerManager.I.trainingSkillLevelDic[VARIABLE.TID];
            }
            // Rate일 때
            else
            {
                rate += VARIABLE.AdditionalVal * PlayerManager.I.trainingSkillLevelDic[VARIABLE.TID];
            }
        }

        foreach (var VARIABLE in attPowerAbilityList)
        {
            // Rate가 아닐 때
            if (VARIABLE.IsRate == 0)
            {
                finalPower += VARIABLE.AdditionalVal * PlayerManager.I.abilitySkillLevelDic[VARIABLE.TID];
            }
            // Rate일 때
            else
            {
                rate += VARIABLE.AdditionalVal * PlayerManager.I.abilitySkillLevelDic[VARIABLE.TID];
            }
        }
        
        return finalPower + finalPower * rate;
    }
    
    public float GetGrowthCriRate()
    {
        var criRateList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.CriRate);
        float additionalVal = 0;
        foreach (var VARIABLE in criRateList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.trainingSkillLevelDic[VARIABLE.TID];
        }
        return baseCriRate * 1 + additionalVal;
    }
    
    public float GetGrowthCriDamageRate()
    {
        var criDmgRateList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.CriDamageRate);
        float additionalVal = 0;
        foreach (var VARIABLE in criDmgRateList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.trainingSkillLevelDic[VARIABLE.TID];
        }
        return baseCriDmgRate * 1 + additionalVal;
    }
    
    public float GetGrowthFinalDamageRate()
    {
        var finalDmgRateTrainingList = DefaultTable.Training.GetList().FindAll(x => x.TrainingType == TrainingType.FinalDamageRate);
        var finalDmgRateAbilityList = DefaultTable.Ability.GetList().FindAll(x => x.AbilityType == AbilityType.FinalDamageRate);
        float additionalVal = 0;
        foreach (var VARIABLE in finalDmgRateTrainingList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.trainingSkillLevelDic[VARIABLE.TID];
        }

        foreach (var VARIABLE in finalDmgRateAbilityList)
        {
            additionalVal += VARIABLE.AdditionalVal * PlayerManager.I.abilitySkillLevelDic[VARIABLE.TID];
        }
        return baseFinalDamageRate + additionalVal;
    }
}