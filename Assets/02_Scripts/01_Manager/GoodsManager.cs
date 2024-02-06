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
    public Unified idleGold;
    public Unified idleStone;
    private void Start()
    {
        gold = new Unified(BigInteger.Parse(DataManager.I.playerData.gold));
        stone = new Unified(BigInteger.Parse(DataManager.I.playerData.stone));

        SetGoldText();
        SetStoneText();
    }

    public Unified GetGold()
    {
        return gold;
    }

    public Unified GetStone()
    {
        return stone;
    }
    public void IncreaseGold(Unified gold)
    {
        this.gold += gold;
        Debug.Log($"gold: {this.gold}");
        SetGoldText();
    }

    public void DecreaseGold(Unified gold)
    {
        this.gold -= gold;
        Debug.Log($"gold: {this.gold}");
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

    public Unified CalIdleGold(double timeGap)
    {
        BigInteger gold = 0;
        foreach (var stageIdle in DefaultTable.StageIdle.GetList())
        {
            gold = (BigInteger)(stageIdle.Gold_Base *
                                Mathf.Pow(StageManager.I.GetCurrnetStage(), stageIdle.Gold_Increase) * timeGap);
        }
        Debug.Log(gold);
        return new Unified(gold);
    }

    public Unified CalIdleStone(float timeGap)
    {
        BigInteger stone = 0;
        foreach (var stageIdle in DefaultTable.StageIdle.GetList())
        {
            stone = (BigInteger)(stageIdle.Stone_Base *
                                 Mathf.Pow(StageManager.I.GetCurrnetStage(), stageIdle.Stone_Increase) * timeGap);
        }

        return new Unified(stone);
    }
}
