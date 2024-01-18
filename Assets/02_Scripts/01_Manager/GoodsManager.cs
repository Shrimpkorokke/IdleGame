using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class GoodsManager : MonoSingleton<GoodsManager>
{
    [SerializeField] private Text txtGold;
    [SerializeField] private Text TxtStone;
    [SerializeField] private BigInteger gold;
    [SerializeField] private BigInteger stone;

    protected override void Awake()
    {
        SetGoldText();
        SetStoneText();
    }

    public void IncreaseGold(BigInteger gold)
    {
        this.gold += gold;
        SetGoldText();
    }

    public void DecreaseGold(BigInteger gold)
    {
        this.gold -= gold;
        SetGoldText();
    }
    
    public void IncreaseStone(BigInteger stone)
    {
        this.stone += stone;
        SetStoneText();
    }

    public void DecreaseStone(BigInteger stone)
    {
        this.stone -= stone;
        SetStoneText();
    }

    public void SetGoldText()
    {
        txtGold.text = gold.BigintToString();
    }

    public void SetStoneText()
    {
        TxtStone.text = stone.BigintToString();
    }
}
