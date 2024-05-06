using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupYes : Popup
{
    [SerializeField, GetComponentInChildrenName("Btn_Close")] private Button btnClose;
    [SerializeField, GetComponentInChildrenName("Btn_BG")] private Button btnBG;
    [SerializeField, GetComponentInChildrenName("Txt_Content")] private Text txtContent;

    protected override void Awake()
    {
        base.Awake();
        btnClose.onClick.AddListener(() =>
        {
            Close();
        });

        btnBG.onClick.AddListener(() =>
        {
            Close();
        }); ;
    }

    public void SetText()
    {
        txtContent.text = "재화가 부족합니다.";
    }

}
