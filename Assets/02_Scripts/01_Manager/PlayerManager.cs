using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public GameObject player;
    public int basePower;
    public float baseSpeed;
    public float baseCriRate;
    public float baseCriDmg;
    public float dmgPower;
    
    public int attPowerLv;
    public int attSpeedLv;
    public int criRateLv;
    public int criDamageLv;
    public int dmgPowerLv;
    
    public int attPowerSuperLv;
    public int attSpeedSuperLv;
    public int criRateSuperLv;
    public int criDamageSuperLv;
    public int dmgPowerSuperLv;
    
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
}
