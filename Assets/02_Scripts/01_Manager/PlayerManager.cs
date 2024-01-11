using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player player;
    public bool isReady;
    public bool isAttack;
    
    public List<TrainingStat> trainingList = new();
    public List<AbilityStat> abilityStatList = new();

    #region Training_Normal_Level
    public int attPowerLv;
    public int attSpeedLv;
    [HideInInspector] public int criRateLv;
    [HideInInspector] public int criDamageLv;
    [HideInInspector] public int dmgPowerLv;
    #endregion

    #region  Training_Super_Level
    [HideInInspector] public int attPowerSuperLv;
    [HideInInspector] public int attSpeedSuperLv;
    [HideInInspector] public int criRateSuperLv;
    [HideInInspector] public int criDamageSuperLv;
    [HideInInspector] public int dmgPowerSuperLv;
    #endregion
    

    #region Ray
    public float rayLength;
    #endregion
    
    // tid, level
    private Dictionary<int, int> skillDic = new();
    private void Awake()
    {
        if(player == null)
            player = GameObject.Find("Player").GetComponent<Player>();

        player.basePower = DefaultTable.PlayerStat.GetList()[0].attPower;
        player.baseSpeed = DefaultTable.PlayerStat.GetList()[0].attSpeed;
        player.baseCriRate = DefaultTable.PlayerStat.GetList()[0].criRate;
        player.baseCriDmg = DefaultTable.PlayerStat.GetList()[0].criDamage;
        player.dmgPower = DefaultTable.PlayerStat.GetList()[0].dmgPower;
        StartCoroutine(WaitforReady());
        
        player.SetAttackSpeed();
    }

    private void Update()
    {
        DetectEnemy();
    }

    private void DetectEnemy()
    {
        if (isReady == false)
            return;
        
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.right, rayLength, LayerMask.GetMask("Enemy"));

        isAttack = hit.collider != null && hit.collider.CompareTag("Enemy") ? true : false;
    }
    
    IEnumerator WaitforReady()
    {
        yield return new WaitForSeconds(3f);
        isReady = true;
    }
    
    void OnDrawGizmos()
    {
        if (player == null || isReady == false)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.transform.position, player.transform.position + Vector3.right * rayLength);
    }

    public void IncreaseGrowth(GrowthButton growthButton)
    {
        int tid = growthButton.TID;
        var a = DefaultTable.Training.GetList().Find(x => x.TID == growthButton.TID);
        if (skillDic.ContainsKey(tid))
        {
            if (skillDic[tid] < a.MaxLevel)
            {
                // 스킬레벨 증가
                skillDic[tid]++;
                
                // UI 관련 스크립트 추가할 것
            }
        }
    }
  
}

public struct TrainingStat
{
    
}

public struct AbilityStat
{
    
}