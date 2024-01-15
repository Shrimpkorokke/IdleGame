using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class ContinuousButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed = false;
    private float initialDelay = 0.5f; // 초기 딜레이
    private float currentDelay; // 현재 딜레이
    private float delayDecrement = 0.05f; // 딜레이 감소량
    private Action buttonAction; 
    private void Start()
    {
        currentDelay = initialDelay;
    }

    private IEnumerator ButtonPressedRoutine()
    {
        while (isButtonPressed)
        {
            ButtonFunction(); // 여기에 버튼이 눌렸을 때 호출할 함수를 넣습니다.
            yield return new WaitForSeconds(currentDelay);
            
            // 딜레이 감소 (최소값 제한)
            if (currentDelay > 0.05f) 
            {
                currentDelay -= delayDecrement;
            }
        }
    }
    
    // 외부에서 호출할 함수 설정
    public void SetButtonAction(Action newAction)
    {
        buttonAction = newAction;
    }
    
    // 버튼이 눌렸을 때 호출될 함수
    private void ButtonFunction()
    {
        buttonAction?.Invoke();
        Debug.Log("Button Pressed");
    }

    // IPointerDownHandler 인터페이스 구현
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isButtonPressed)
        {
            isButtonPressed = true;
            currentDelay = initialDelay;
            StartCoroutine(ButtonPressedRoutine());
        }
    }

    // IPointerUpHandler 인터페이스 구현
    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
