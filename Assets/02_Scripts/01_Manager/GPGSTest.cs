using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPGSTest : MonoBehaviour
{
    [SerializeField] private Text log;
    [SerializeField] private Button btnLogin;
    [SerializeField] private Button btnLogout;

    [SerializeField] private Button btnSave;
    [SerializeField] private Button btnLoad;
    [SerializeField] private Button btnDelete;

    private void Awake()
    {
        // 로그인
        btnLogin.onClick.AddListener(() =>
        {
            GPGSManager.I.Login((success, localUser) =>
                log.text = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");
        });
        
        // 로그아웃
        btnLogout.onClick.AddListener(() =>
        {
            GPGSManager.I.Logout();
        });
        
        // 세이브 클라우드
        btnSave.onClick.AddListener(() =>
        {
            GPGSManager.I.SaveCloud("mysave", "want data", success => log.text = $"{success}");
        });
        
        btnLoad.onClick.AddListener(() =>
        {
            GPGSManager.I.LoadCloud("mysave", (success, data) => log.text = $"{success}, {data}");
        });
        
        btnDelete.onClick.AddListener(() =>
        {
            GPGSManager.I.DeleteCloud("mysave", success => log.text = $"{success}");
        });
    }
}