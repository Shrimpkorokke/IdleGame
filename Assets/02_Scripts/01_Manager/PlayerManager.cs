using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [HideInInspector] public bool isReady;
    [HideInInspector] public GameObject player;
    public int basePower;
    public float baseSpeed;
    [HideInInspector] public float baseCriRate;
    [HideInInspector] public float baseCriDmg;
    [HideInInspector] public float dmgPower;
    
    public int attPowerLv;
    public int attSpeedLv;
    [HideInInspector] public int criRateLv;
    [HideInInspector] public int criDamageLv;
    [HideInInspector] public int dmgPowerLv;
    
    [HideInInspector] public int attPowerSuperLv;
    [HideInInspector] public int attSpeedSuperLv;
    [HideInInspector] public int criRateSuperLv;
    [HideInInspector] public int criDamageSuperLv;
    [HideInInspector] public int dmgPowerSuperLv;
    
    private void Awake()
    {
        if(player == null)
            player = GameObject.Find("Player");

        basePower = DefaultTable.PlayerStat.GetList()[0].attPower;
        baseSpeed = DefaultTable.PlayerStat.GetList()[0].attSpeed;
        baseCriRate = DefaultTable.PlayerStat.GetList()[0].criRate;
        baseCriDmg = DefaultTable.PlayerStat.GetList()[0].criDamage;
        dmgPower = DefaultTable.PlayerStat.GetList()[0].dmgPower;
    }

    IEnumerator AAA()
    {
        yield return new WaitForSeconds(3f);
        isReady = true;
    }
    
    public float GetAttSpeed()
    {
        var a = DefaultTable.Training.TrainingList.FirstOrDefault(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                                       x.TrainingType == TrainingType.AttSpeed);
        return baseSpeed - a.AdditionalVal;
    }
}
