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
                return;
            else
                PopupManager.I.GetPopup<PopupShop>().Open();
        });
        
        btnGrowth.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupGrowth>())
                return;
            else
                PopupManager.I.GetPopup<PopupGrowth>().Open();
        });
        
        btnEquipment.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupEquipment>())
                return;
            else
                PopupManager.I.GetPopup<PopupEquipment>().Open();
        });
        
        btnDungeon.onClick.AddListener(() =>
        {
            if (PopupManager.I.IsPopupOpen<PopupDungeon>())
                return;
            else
                PopupManager.I.GetPopup<PopupDungeon>().Open();
        });
    }
}
