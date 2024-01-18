using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class GoodsManager : MonoSingleton<GoodsManager>
{
    [SerializeField] private Text txtGold;
    [SerializeField] private Text TxtStone;
    [SerializeField] private Unified gold = new();
    [SerializeField] private Unified stone = new();

    protected override void Awake()
    {
        SetGoldText();
        SetStoneText();
    }

    public Unified GetGold()
    {
        return gold;
    }
    
    public void IncreaseGold(Unified gold)
    {
        this.gold += gold;
        SetGoldText();
    }

    public void DecreaseGold(Unified gold)
    {
        this.gold -= gold;
        SetGoldText();
    }
    
    public void IncreaseStone(Unified stone)
    {
        this.stone += stone;
        SetStoneText();
    }

    public void DecreaseStone(Unified stone)
    {
        this.stone -= stone;
        SetStoneText();
    }

    public void SetGoldText()
    {
        txtGold.text = gold.IntPart.BigintToString();
    }

    public void SetStoneText()
    {
        TxtStone.text = stone.IntPart.BigintToString();
    }
}
