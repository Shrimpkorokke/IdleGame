using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Serialization;
using UniRx;

public class GrowthButton : MonoBehaviour
{
    [GetComponentInChildren] public ContinuousButton btn;

    [GetComponentInChildrenName("Txt_Level")] public Text txtLevel;
    [GetComponentInChildrenName("Cover_Unlock")] public GameObject coverUnlock;

    public GrowthType growthType;
    public TrainingGrade trainingGrade;
    public TrainingType trainingType;
    public int TID;
    private bool isUnlock;
    public void Init()
    {
        //btn.onClick.AddListener(()=>PlayerManager.I.IncreaseGrowth(this));
        if(btn != null)
            btn.SetButtonAction(() => PlayerManager.I.IncreaseGrowth(this));
        SubscribeToDictionary();
    }
    
    public void SubscribeToDictionary()
    {
        var training = DefaultTable.Training.GetList().Find(x => x.TID == TID);
        var unlockLevel = training.UnlockLevel;
        
        PlayerManager.I.skillLevelDic.ObserveReplace()
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
}
