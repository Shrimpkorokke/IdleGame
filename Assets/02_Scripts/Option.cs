using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private Button btnSpeed;
    [SerializeField] private Button btnOption;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnPowerSaving;
    [SerializeField] private Button btnTitleExit;
    
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject powerSaving;
    private void Awake()
    {
        btnOption.onClick.AddListener(() => option.SetActive(true));
        btnSetting.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupSetting>())
                PopupManager.I.GetPopup<PopupSetting>().Close();
            else
                PopupManager.I.GetPopup<PopupSetting>().Open();
        });
        
        btnPowerSaving.onClick.AddListener(() => powerSaving.SetActive(true));
        btnTitleExit.onClick.AddListener(() => option.SetActive(false));
        
        

    }
}
