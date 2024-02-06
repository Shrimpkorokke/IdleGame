using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Serialization;

public class GPGSManager : MonoBehaviour
{
    [SerializeField] private Text txtLog;
    [SerializeField] private Button btnLogin;
    void Start()
    {
        btnLogin.onClick.AddListener(() => Login());
    }

    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            if (result == SignInStatus.Success)
            {
                // 유저가 변경 가능
                string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();
                // 유저가 변경 불가능
                string userID = PlayGamesPlatform.Instance.GetUserId();
                txtLog.text = $"로그인 성공 : {displayName} {userID}";
            }
            else
            {
                txtLog.text = "로그인 실패";
            }
        });
    }
}
