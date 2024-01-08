using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField, GetComponentInChildrenName("Btn_Shop")] private Button btnShop;
    [SerializeField, GetComponentInChildrenName("Btn_Growth")] private Button btnGrowth;
    [SerializeField, GetComponentInChildrenName("Btn_Equipment")] private Button btnEquipment;
    [SerializeField, GetComponentInChildrenName("Btn_Dungeon")] private Button btnDungeon;

    private void Awake()
    {
        btnShop.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupShop>())
                PopupManager.I.GetPopup<PopupShop>().Close();
            else
                PopupManager.I.GetPopup<PopupShop>().Open();
        });
        
        btnGrowth.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupGrowth>())
                PopupManager.I.GetPopup<PopupGrowth>().Close();
            else
                PopupManager.I.GetPopup<PopupGrowth>().Open();
        });
        
        btnEquipment.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupEquipment>())
                PopupManager.I.GetPopup<PopupEquipment>().Close();
            else
                PopupManager.I.GetPopup<PopupEquipment>().Open();
        });
        
        btnDungeon.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupDungeon>())
                PopupManager.I.GetPopup<PopupDungeon>().Close();
            else
                PopupManager.I.GetPopup<PopupDungeon>().Open();
        });
    }
}
