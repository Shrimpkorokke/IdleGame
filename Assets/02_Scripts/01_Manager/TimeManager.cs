using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    public float idleTimeLimit = 20f; // 유저가 비활성 상태로 간주되기 전까지의 시간 (초 단위)
    public int reducedFrameRate = 30; // 비활성 상태일 때의 프레임 레이트
    private float lastInputTime; // 마지막으로 입력이 감지된 시간
    private bool isIdle;

    void Start()
    {
        // 초기 마지막 입력 시간을 현재 시간으로 설정
        lastInputTime = Time.time;
    }

    void Update()
    {
        // 사용자 입력 감지
        if (Input.anyKey || Input.touchCount > 0)
        {
            lastInputTime = Time.time;
            
            if (isIdle == true)
            {
                isIdle = false;
                // 프레임 레이트를 기본값으로 재설정
                Application.targetFrameRate = -1;
                PopupManager.I.GetPopup<PopupIdle>().Close();
                
            }
        }

        // 유저가 설정한 시간 동안 아무런 동작을 하지 않은 경우 프레임 레이트를 낮춤
        if (Time.time - lastInputTime > idleTimeLimit)
        {
            if (isIdle == false)
            {
                isIdle = true;
                PopupManager.I.GetPopup<PopupIdle>().Open();
                Application.targetFrameRate = reducedFrameRate;
            }
        }
    }
}