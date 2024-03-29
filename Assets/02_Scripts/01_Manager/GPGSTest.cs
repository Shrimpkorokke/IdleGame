using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField] private Button btnStartGame;
    private void Awake()
    {
        // 로그인
        btnLogin.onClick.AddListener(() =>
        {
            Debug.Log("!!!! Btn_Login 눌림");
            GPGSManager.I.Login((success, localUser) =>
            {
                Debug.Log("!!!! Login 콜백 불림");
                log.text = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}";
                
            });
        });
        
        // 로그아웃
        btnLogout.onClick.AddListener(() =>
        {
            GPGSManager.I.Logout();
        });
        
        // 세이브 클라우드
        btnSave.onClick.AddListener(() =>
        {
            GPGSManager.I.SaveCloud("PickaxeIdle_PlayerData", "json", success => log.text = $"{success}");
        });
        
        // 만약 트루인데 데이터가 empty라면 이제 메타데이터를 생성한다.
        // 트로일 때 데이터가 존재하면 데이터를 바탕으로 세팅한다.
        btnLoad.onClick.AddListener(() =>
        {
            Debug.Log("!!!! Btn_Load 눌림");
            //GPGSManager.I.LoadCloud("PickaxeIdle_PlayerData", (success, data) => log.text = $"{success}, {data}");
            LoadCloud();
        });
        
        btnDelete.onClick.AddListener(() =>
        {
            GPGSManager.I.DeleteCloud("PickaxeIdle_PlayerData", success => log.text = $"{success}");
        });
        btnLoadScene.onClick.AddListener(() =>
        {
            LoadingSceneController.LoadScene("SceneBattle");
        });
        btnStartGame.onClick.AddListener(() =>
        {
            LoadingSceneController.LoadScene("SceneBattle");
        });
    }

    private void Start()
    {
        Debug.Log("!!!! start");
        // 자동 로그인으로 변경
        GPGSManager.I.Login((success, localUser) =>
        {
            Debug.Log("!!!! Login 콜백 불림");
            log.text = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}";
            LoadCloud();
        });
    }

    public void LoadCloud()
    {
        Debug.Log("!!!! LoadCloud 불림");
        // 로드 데이터
        GPGSManager.I.LoadCloud("PickaxeIdle_PlayerData", (success, data) =>
        {
            log.text = $"{success}, {data}";
            Debug.Log("!!!! LoadCloud 콜백 불림");
            if (success) 
            {
                // 데이터가 있을 때
                if (string.IsNullOrEmpty(data) == false)
                {
                    Debug.Log($"!!!! 데이터가 있음: {data}");
                    DataManager.I.JsontoPlayerData(data, () => ShowBtnStart());
                }
                // 데이터가 없을 때
                else
                {
                    Debug.Log($"!!!! 데이터가 없음: {data}");
                    DataManager.I.SetCloudMetaData();
                    SaveCloud(() => ShowBtnStart());
                }
            }
            else
            {
                // 오류 표시
            }
        });
    }

    public void SaveCloud(Action callback)
    {
        GPGSManager.I.SaveCloud("PickaxeIdle_PlayerData", DataManager.I.GetJsonPlayerData(), success =>
        {
            if(success) 
            {
                callback?.Invoke();
                log.text = $"{success}";
            }
        });
    }

    public void ShowBtnStart()
    {
        btnStartGame.gameObject.SetActive(true);
    }
}