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
        txtContent.text = success ? "���������� ������ �Ϸ��߽��ϴ�." : "���忡 �����߽��ϴ�.\n����� �ٽ� �õ����ּ���.";
    }

}
