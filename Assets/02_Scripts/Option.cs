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
    [SerializeField] private Button btnSave;
    [SerializeField] private Button btnTitleExit;
    [SerializeField] private Button btnBG;
    [SerializeField] private GameObject option;
    private void Awake()
    {
        DeletePLayerPrefs();
        Application.runInBackground = true;

        btnOption.onClick.AddListener(() => option.SetActive(true));
        btnSetting.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupSetting>())
                PopupManager.I.GetPopup<PopupSetting>().Close();
            else
                PopupManager.I.GetPopup<PopupSetting>().Open();
            
            option.SetActive(false);
        });
        
        btnPowerSaving.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupIdle>())
                PopupManager.I.GetPopup<PopupIdle>().Close();
            else
            {
                PopupManager.I.GetPopup<PopupIdle>().Open();
                TimeManager.I.AutoSaving();
            }
            
            option.SetActive(false);
        });

        btnSave.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupSaveResult>())
                PopupManager.I.GetPopup<PopupSaveResult>().Close();
#if UNITY_EDITOR
            DataManager.I.SaveLocalPlayerData();
            DataManager.I.SaveLocalOptionData();
#else
            DataManager.I.SaveCloud((success) =>
            {
                if (PopupManager.I.IsPopupOpen<PopupSaveResult>())
                    PopupManager.I.GetPopup<PopupSaveResult>().Close();
                var popupResult = PopupManager.I.GetPopup<PopupSaveResult>();
                popupResult.Open();
                popupResult.SetText(success);

            });
#endif
        });

        btnTitleExit.onClick.AddListener(() => option.SetActive(false));
        btnBG.onClick.AddListener(() => option.SetActive(false));
    }
    
    private void DeletePLayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
