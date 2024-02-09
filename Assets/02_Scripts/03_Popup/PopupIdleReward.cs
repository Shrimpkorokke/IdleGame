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

        });

        btnObtain.onClick.AddListener(() =>
        {
            GoodsManager.I.ObtainIdleGoods(false);
            DataManager.I.SaveCloud();
            this.Close();
        });
    }
}
