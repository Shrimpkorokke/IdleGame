using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopupGrowth : Popup
{
    [SerializeField, GetComponentInChildrenName("Txt_Title")] private Text txtTitle;
    [SerializeField, GetComponentInChildrenName("Btn_Training")] private Button btnTraining;
    [SerializeField, GetComponentInChildrenName("Btn_Ability")] private Button btnAbility;

    [SerializeField, GetComponentInChildrenName("Content_Training")] private GameObject contentTraining;
    [SerializeField, GetComponentInChildrenName("Content_Ability")] private GameObject contentAbility;

    protected override void Awake()
    {
        base.Awake();
        btnTraining.onClick.AddListener(() => ToggleContent(GrowthType.Training));
        btnAbility.onClick.AddListener(() => ToggleContent(GrowthType.Ability));
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        ToggleContent(GrowthType.Training);
    }

    private void ToggleContent(GrowthType growthType)
    {
        // 일단은 이렇게 나중에 localization 테이블 만들어서 사용하기
        if (growthType == GrowthType.Training)
        {
            txtTitle.text = Const.TRAINING;
        }
        else if (growthType == GrowthType.Ability)
        {
            txtTitle.text = Const.ABILITY;
        }
        
        contentTraining.SetActive(growthType == GrowthType.Training);
        contentAbility.SetActive(growthType == GrowthType.Ability);
    }
}
