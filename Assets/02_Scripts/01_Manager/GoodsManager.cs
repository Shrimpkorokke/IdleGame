using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoodsManager : MonoSingleton<GoodsManager>
{
    [SerializeField] private Text txtGold;
    [SerializeField] private Text TxtStone;
    [SerializeField] private int gold;
    [SerializeField] private int stone;

    protected override void Awake()
    {
        SetGoldText();
        SetStoneText();
    }

    public void IncreaseGold(int gold)
    {
        this.gold += gold;
        SetGoldText();
    }

    public void DecreaseGold(int gold)
    {
        this.gold -= gold;
        SetGoldText();
    }
    
    public void IncreaseStone(int stone)
    {
        this.stone += stone;
        SetStoneText();
    }

    public void DecreaseStone(int stone)
    {
        this.stone -= stone;
        SetStoneText();
    }

    public void SetGoldText()
    {
        txtGold.text = gold.ToString();
    }

    public void SetStoneText()
    {
        TxtStone.text = stone.ToString();
    }
}
