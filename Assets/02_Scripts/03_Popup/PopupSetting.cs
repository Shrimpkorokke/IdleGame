using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : Popup
{
    [SerializeField, GetComponentInChildrenName("Btn_Exit")] private Button btnExit;

    [SerializeField, GetComponentInChildrenName("Btn_BG")] private Button btnBG;

    [SerializeField, GetComponentInChildrenName("Btn_Shaking")] private Button btnShaking;
    [SerializeField, GetComponentInChildrenName("Btn_PowerSaving")] private Button btnPowerSaving;
    [SerializeField, GetComponentInChildrenName("Btn_Google")] private Button btngoogle;

    protected override void Awake()
    {
        btnExit.onClick.AddListener(() => Close());
        btnBG.onClick.AddListener(() => Close());
    }
}
