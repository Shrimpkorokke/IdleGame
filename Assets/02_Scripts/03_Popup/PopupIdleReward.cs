using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupIdleReward : Popup
{
    [SerializeField, GetComponentInChildrenName("Txt_GoldReward")] private Text txtGold;
    [SerializeField, GetComponentInChildrenName("Txt_StoneReward")] private Text txtStone;

    [SerializeField, GetComponentInChildrenName("Btn_Bonus")] private Button btnBonus;
    [SerializeField, GetComponentInChildrenName("Btn_Obtain")] private Button btnObtain;
    [SerializeField, GetComponentInChildrenName("Btn_BG")] private Button btnBG;

    public void Init()
    {
        txtGold.text = GoodsManager.I.idleGold.IntPart.BigintToString();
        txtStone.text = GoodsManager.I.idleStone.IntPart.BigintToString();
    }

    protected override void Awake()
    {
        base.Awake();
        btnBonus.onClick.AddListener(() =>
        {
            TimeManager.I.Pause();
            RewardedAds.I.LoadAd();
        });

        btnObtain.onClick.AddListener(() =>
        {
            GoodsManager.I.ObtainIdleGoods(false);
#if !UNITY_EDITOR
            DataManager.I.SaveCloud();
#endif
            this.Close();
        });

        btnBG.onClick.AddListener(() => 
        {
            GoodsManager.I.ObtainIdleGoods(false);
#if !UNITY_EDITOR
            DataManager.I.SaveCloud();
#endif
            this.Close();
        });
    }
    
    public void GetBonusGoods()
    {
        TimeManager.I.ReturntoPreSpeed();
        GoodsManager.I.ObtainIdleGoods(true);
        DataManager.I.SaveCloud();
    }
}
