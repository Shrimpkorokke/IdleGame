using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSaveResult : Popup
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

    public void SetText(bool success)
    {
        txtContent.text = success ? "성공적으로 저장을 완료했습니다." : "저장에 실패했습니다.\n잠시후 다시 시도해주세요.";
    }

}
