using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Numerics;
using UnityEngine.Serialization;

public class AbilityButton : MonoBehaviour
{
    [GetComponentInChildren] public ContinuousButton btn;

    [GetComponentInChildrenName("Txt_Level")] public Text txtLevel;
    [GetComponentInChildrenName("Cover_Unlock")] public GameObject coverUnlock;
    [GetComponentInChildrenName("Txt_Point")] public Text txtPoint;
    
    public GrowthType growthType;
    public AbilityType abilityType;
    public int TID;
    private bool isUnlock;
    public void Init()
    {
        //btn.onClick.AddListener(()=>PlayerManager.I.IncreaseGrowth(this));
        if(btn != null)
            btn.SetButtonAction(() => PlayerManager.I.IncreaseAbility(this));
        SubscribeToDictionary();
        SetPointTxt();
    }
    
    public void SubscribeToDictionary()
    {
        var ability = DefaultTable.Ability.GetList().Find(x => x.TID == TID);
        var unlockLevel = ability.UnlockLevel;
        
        PlayerManager.I.abilitySkillLevelDic.ObserveReplace()
            .Where(change => change.Key == ability.UnlockTID)
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

    public int GetRequiredPoint()
    {
        var ability = DefaultTable.Ability.GetList().Find(x => x.TID == TID);
        int requiredPoint = ability.LevelUpPoint;
        Debug.Log(requiredPoint);
        return requiredPoint;
    }
    
    public void SetPointTxt()
    {
        txtPoint.text = GetRequiredPoint().ToString();
    }
}
