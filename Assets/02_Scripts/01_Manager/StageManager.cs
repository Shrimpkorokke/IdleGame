using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoSingleton<StageManager>
{
    private int currentStage = 1;
    private float maxTime = 30;
    private float currentTime;
    private int count;
    
    [SerializeField] private Text txtStage;
    [SerializeField] private Slider sliderGuage;
    

    private void Start()
    {
        currentStage = DataManager.I.playerData.stage;
        SetTextStage();
    }
    private void Update()
    {
        if (SpawnManager.I.bossSpawned && PlayerManager.I.isAttack)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                sliderGuage.value = 0;
                FailBoss();
            }
            else
            {
                sliderGuage.value = currentTime / maxTime;
            }
        }
    }

    public void IncreaseStage()
    {
        currentStage++;
        DataManager.I.playerData.stage = currentStage;
        SetTextStage();
    }

    public int GetCurrnetStage()
    {
        return currentStage;
    }

    public void SetTextStage()
    {
        txtStage.text = currentStage.ToString();
    }

    public void IncreaseCount(int count)
    {
        if (this.count > 100)
            return;
        
        this.count += count;
        if (this.count >= 100)
        {
            sliderGuage.value = 1;
            SpawnManager.I.ShowBtnBoss(true);
            currentTime = maxTime;
        }
        else
        {
            sliderGuage.value = (float)this.count / 100;
        }
    }
    public void DieBoss()
    {
        IncreaseStage();
        count = 0;
        sliderGuage.value = 0;
    }

    public void FailBoss()
    {
        SpawnManager.I.FailBoss();
        CameraFade.I.StartFade();
        count = 0;
        sliderGuage.value = 0;
    }
}
