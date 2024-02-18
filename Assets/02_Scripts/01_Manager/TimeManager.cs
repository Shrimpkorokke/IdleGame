using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoSingleton<TimeManager>
{
    public float idleTimeLimit = 20f; // 유저가 비활성 상태로 간주되기 전까지의 시간 (초 단위)
    public int reducedFrameRate = 30; // 비활성 상태일 때의 프레임 레이트
    private float lastInputTime; // 마지막으로 입력이 감지된 시간
    public bool isIdle;
    private DateTime idleStartTime; // 비활성 상태가 시작된 시간
    
    void Start()
    {
        lastInputTime = Time.time;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            lastInputTime = Time.time;

            if (isIdle == true)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    //PopupManager.I.GetPopup<PopupIdle>().Close();
                    isIdle = false;
                    Application.targetFrameRate = -1;
                    Time.timeScale = 1;
                    // 비활성 상태에서 활성 상태로 전환 시 경과한 시간 계산 및 출력
                    TimeSpan timeSpan = DateTime.Now - idleStartTime;
                    
                    var gap = timeSpan.TotalSeconds > DefaultTable.StageIdle.GetList()[0].Max_Time ? DefaultTable.StageIdle.GetList()[0].Max_Time : timeSpan.TotalSeconds;
                    GoodsManager.I.CalIdleGoods(gap);
                    var popupReward = PopupManager.I.GetPopup<PopupIdleReward>();
                    popupReward.Open();
                    popupReward.Init();

                    Debug.Log($"User was idle for {timeSpan.TotalSeconds} seconds.");
                }
            }
        }

        if (Time.time - lastInputTime > idleTimeLimit)
        {
            if (DataManager.I.optionData.autoPowerSaving == false)
                return;

            if (isIdle == false)
            {
                isIdle = true;
                PopupManager.I.GetPopup<PopupIdle>().Open();
                Application.targetFrameRate = reducedFrameRate;
                Time.timeScale = 0.5f;

                // 현재 날짜 및 시간 저장
                idleStartTime = DateTime.Now;
            }
        }
    }
}