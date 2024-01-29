using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Numerics;
using UnityEngine.Serialization;
using UniRx;

public class TrainingButton : MonoBehaviour
{
    [GetComponentInChildren] public ContinuousButton btn;

    [GetComponentInChildrenName("Txt_Level")] public Text txtLevel;
    [GetComponentInChildrenName("Cover_Unlock")] public GameObject coverUnlock;
    [GetComponentInChildrenName("Txt_Gold")] public Text txtGold;
    
    public GrowthType growthType;
    public TrainingGrade trainingGrade;
    public TrainingType trainingType;
    public int TID;
    private bool isUnlock;
    public void Init()
    {
        //btn.onClick.AddListener(()=>PlayerManager.I.IncreaseGrowth(this));
        if(btn != null)
            btn.SetButtonAction(() => PlayerManager.I.IncreaseTraining(this));
        SubscribeToDictionary();
        SetGoldTxt();
    }
    
    public void SubscribeToDictionary()
    {
        var training = DefaultTable.Training.GetList().Find(x => x.TID == TID);
        var unlockLevel = training.UnlockLevel;
        
        PlayerManager.I.trainingSkillLevelDic.ObserveReplace()
            .Where(change => change.Key == training.UnlockTID)
            .Subscribe(change =>
            {
                if (change.NewValue >= unlockLevel)
                {
                    AAAA();
                }
            }).AddTo(this);
    }

    public void AAAA()
    {
        coverUnlock.SetActive(false);
    }

    public Unified GetRequiredGold()
    {
        var training = DefaultTable.Training.GetList().Find(x => x.TID == TID);
        BigInteger requiredGold = (BigInteger)(Mathf.RoundToInt(training.LevelUpGold * Mathf.Pow(PlayerManager.I.trainingSkillLevelDic[TID] + 1, training.LevelUpGoldIncrease)));
        return new Unified(requiredGold);
    }
    
    public void SetGoldTxt(bool isMaxLevel = false)
    { 
        txtGold.text = isMaxLevel ? "Max" : GetRequiredGold().IntPart.BigintToString();
    }
}
