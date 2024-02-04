using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public enum PopupType
    {
        Main, Setting, Idle
    }

    public PopupType popupType;
    protected virtual void Awake()
    {
        // 팝업 생성 시 PopupManager에 자신을 등록
        PopupManager.I.RegisterPopup(this);
    }

    // 팝업이 파괴될 때 PopupManager에 자신을 해제
    protected virtual void OnDestroy()
    {
        PopupManager.I.UnregisterPopup(this);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        PopupManager.I.isPopupOpened = true;
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        PopupManager.I.isPopupOpened = false;
    }
}
