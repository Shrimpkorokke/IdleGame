using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player player;
    public bool isReady;
    public bool isAttack;

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
    public ReactiveDictionary<int, int> skillLevelDic = new();
    private void Awake()
    {
        if(player == null)
            player = GameObject.Find("Player").GetComponent<Player>();

        player.basePower = DefaultTable.PlayerStat.GetList()[0].attPower;
        player.baseSpeed = DefaultTable.PlayerStat.GetList()[0].attSpeed;
        player.baseCriRate = DefaultTable.PlayerStat.GetList()[0].criRate;
        player.baseCriDmgRate = DefaultTable.PlayerStat.GetList()[0].criDamage;
        player.baseFinalDamageRate = DefaultTable.PlayerStat.GetList()[0].finalDamage;
        
        foreach (var VARIABLE in DefaultTable.Training.GetList())
        {
            // 추후 저장된 값을 집어 넣기 
            skillLevelDic[VARIABLE.TID] = 0;
        }
        
        player.SetAttackSpeed();
        
        StartCoroutine(WaitforReady());
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
        var training = DefaultTable.Training.GetList().Find(x => x.TID == tid);
        
        if (skillLevelDic.ContainsKey(tid) == false)
            return;
        
        if (skillLevelDic[tid] >= training.MaxLevel)
            return;
        
        skillLevelDic[tid]++;
        
        // UI 관련
        if (skillLevelDic[tid] < training.MaxLevel)
        {
            growthButton.txtLevel.text = $"Lv.{skillLevelDic[tid]}";
        }
        else
        {
            growthButton.txtLevel.text = $"Lv.Max";
        }
        growthButton.SetGoldTxt();
        if (training.TrainingType == TrainingType.AttSpeed)
        {
            player.SetAttackSpeed();
        }
    }
}