using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [HideInInspector] public bool isReady;
    [HideInInspector] public Player player;
    
    
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
            player = GameObject.Find("Player").GetComponent<Player>();

        player.basePower = DefaultTable.PlayerStat.GetList()[0].attPower;
        player.baseSpeed = DefaultTable.PlayerStat.GetList()[0].attSpeed;
        player.baseCriRate = DefaultTable.PlayerStat.GetList()[0].criRate;
        player.baseCriDmg = DefaultTable.PlayerStat.GetList()[0].criDamage;
        player.dmgPower = DefaultTable.PlayerStat.GetList()[0].dmgPower;
        StartCoroutine(AAA());
    }

    IEnumerator AAA()
    {
        yield return new WaitForSeconds(3f);
        isReady = true;
    }
    
    
}
