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
    [SerializeField] private Button btnLoadScene;


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
            GPGSManager.I.SaveCloud("PickaxeMaster_PlayerData", "json", success => log.text = $"{success}");
        });
        
        // 만약 트루인데 데이터가 empty라면 이제 메타데이터를 생성한다.
        // 트로일 때 데이터가 존재하면 데이터를 바탕으로 세팅한다.
        btnLoad.onClick.AddListener(() =>
        {
            GPGSManager.I.LoadCloud("PickaxeMaster_PlayerData", (success, data) => log.text = $"{success}, {data}");
        });
        
        btnDelete.onClick.AddListener(() =>
        {
            GPGSManager.I.DeleteCloud("mysave", success => log.text = $"{success}");
        });
        btnLoadScene.onClick.AddListener(() =>
        {
            LoadingSceneController.LoadScene("SceneBattle");
        });
    }
}