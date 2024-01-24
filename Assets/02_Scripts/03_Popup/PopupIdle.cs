using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupIdle : Popup
{
    [SerializeField, GetComponentInChildrenName("Txt_Time")] private Text txtTime;

    private void Update()
    {
        DateTime now = DateTime.Now;

        // 시간:분 형식으로 포맷팅
        txtTime.text = now.ToString("HH:mm");

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Close();
            }
        }
    }
}
