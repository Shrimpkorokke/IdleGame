using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player player;
    public bool isReady;
    public bool isAttack;
    public bool isTest;

    #region Level
    [SerializeField] private int level = 1;
    public int currentExp;
    public int needExp;
    public int abilityPoint;
    #endregion
    
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
    public ReactiveDictionary<int, int> trainingSkillLevelDic = new();
    public ReactiveDictionary<int, int> abilitySkillLevelDic = new();

    [SerializeField] private Text txtLevel;
    private void Awake()
    {
        if(player == null)
            player = GameObject.Find("Player").GetComponent<Player>();

        player.basePower = DefaultTable.PlayerStat.GetList()[0].attPower;
        player.baseSpeed = DefaultTable.PlayerStat.GetList()[0].attSpeed;
        player.baseCriRate = DefaultTable.PlayerStat.GetList()[0].criRate;
        player.baseCriDmgRate = DefaultTable.PlayerStat.GetList()[0].criDamage;
        player.baseFinalDamageRate = DefaultTable.PlayerStat.GetList()[0].finalDamage;
        
        // 훈련 레벨 딕셔너리
        foreach (var VARIABLE in DefaultTable.Training.GetList())
        {
            // 추후 저장된 값을 집어 넣기 
            trainingSkillLevelDic[VARIABLE.TID] = 0;
        }
        
        // 특성 레벨 딕셔너리
        foreach (var VARIABLE in DefaultTable.Ability.GetList())
        {
            // 추후 저장된 값을 집어 넣기 
            abilitySkillLevelDic[VARIABLE.TID] = 0;
        }
        
        
        // 필요 경험치 설정
        foreach (var VARIABLE in DefaultTable.Level.GetList())
        {
            needExp = level * VARIABLE.Exp_Need;
        }

        SetTextLevel();
        
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

    public void IncreaseTraining(TrainingButton trainingButton)
    {
        int tid = trainingButton.TID;
        var training = DefaultTable.Training.GetList().Find(x => x.TID == tid);
        
        if (trainingSkillLevelDic.ContainsKey(tid) == false)
            return;
        
        // MaxLevel 일 때
        if (trainingSkillLevelDic[tid] >= training.MaxLevel)
            return;

        if (isTest == false)
        {
            // 돈이 부족할 때 
            if (GoodsManager.I.GetGold().CompareTo(trainingButton.GetRequiredGold()) <= 0)
                return;
        
            GoodsManager.I.DecreaseGold(trainingButton.GetRequiredGold()); 
        }
        
        trainingSkillLevelDic[tid]++;
        
        // UI 관련
        if (trainingSkillLevelDic[tid] < training.MaxLevel)
        {
            trainingButton.txtLevel.text = $"Lv.{trainingSkillLevelDic[tid]}";
            trainingButton.SetGoldTxt(false);
        }
        else
        {
            trainingButton.txtLevel.text = $"Lv.Max";
            trainingButton.SetGoldTxt(true);
        }
        
        if (training.TrainingType == TrainingType.AttSpeed)
        {
            player.SetAttackSpeed();
        }
    }

    public void IncreaseAbility(AbilityButton abilityButton)
    {
        int tid = abilityButton.TID;
        var ability = DefaultTable.Ability.GetList().Find(x => x.TID == tid);
        
        if (abilitySkillLevelDic.ContainsKey(tid) == false)
            return;
        
        // MaxLevel 일 때
        if (abilitySkillLevelDic[tid] >= ability.MaxLevel)
            return;

        if (isTest == false)
        {
            // 포인트가 부족할 때
            if (abilityPoint < abilityButton.GetRequiredPoint())
                return;
        
            // 포인트 감소
            abilityPoint--; 
        }
        
        abilitySkillLevelDic[tid]++;
        
        // UI 관련
        if (abilitySkillLevelDic[tid] < ability.MaxLevel)
        {
            abilityButton.txtLevel.text = $"Lv.{abilitySkillLevelDic[tid]}";
            abilityButton.SetPointTxt(false);
        }
        else
        {
            abilityButton.txtLevel.text = $"Lv.Max";
            abilityButton.SetPointTxt(true);
        }
    }

    public void IncreaseExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= needExp)
        {
            IncreaseLevelUp();
        }
    }

    public void IncreaseLevelUp()
    {
        currentExp = 0;
        level++;
        abilityPoint++;
        SetTextLevel();
        foreach (var VARIABLE in DefaultTable.Level.GetList())
        {
            needExp = level * VARIABLE.Exp_Need;
        }
        Debug.Log($"Levelup: level{level} currentExp: {currentExp}, needExp: {needExp}");
    }

    public void SetAbilityPoint()
    {
        
    }

    public void SetTextLevel()
    {
        txtLevel.text = $"Lv.{level.ToString()}";
    }
}