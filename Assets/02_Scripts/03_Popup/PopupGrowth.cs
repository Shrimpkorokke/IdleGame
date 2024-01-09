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

    private enum GrowthType {Training, Ability}

    private GrowthType growthType;
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        growthType = GrowthType.Training;
    }

    private void ToggleContent(GrowthType growthType)
    {
        txtTitle.text = "";
    }
}
