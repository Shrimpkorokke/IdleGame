using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class DataManager : MonoSingleton<DataManager>
{
    public PlayerData playerData = new();

    private string path;
    protected override void Awake()
    {
        path = Application.persistentDataPath + "/";
        LoadData();
    }
    
    public void LoadData()
    {
        string data = "";
        // 파일이 있으면 불러옴
        if (File.Exists(path + nameof(PlayerData)))
        {
            data = File.ReadAllText(path + nameof(PlayerData));
        }
        // 파일이 없으면 메타데이터 생성 후 저장
        else
        {
            playerData = new()
            {
                nickName = "Test",
                // 무기
                ownedWeapons = new(){0}, currentWeapon = 0,
                // 재화, 포인트
                gold = "0", stone = "0", point = "0",
                //레벨, exp
                level = "0", exp = "100",
                // 옵션
                bgmValue = 1, sfxValue = 1, shaking = true, autoPowerSaving = true
            };
            data = JsonUtility.ToJson(this.playerData);
            File.WriteAllText(path + nameof(PlayerData), data);
        }
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }
    
    public void SaveData()
    {
        // Data => Json
        string data = JsonUtility.ToJson(this.playerData);
        
        // 저장
        File.WriteAllText(path + nameof(PlayerData), data);
    }
}

public struct PlayerData
{
    #region Player
    // 닉네임
    public string nickName;
    
    // 무기
    public List<int> ownedWeapons;
    public int currentWeapon;
    
    // 재화, 포인트
    public string gold;
    public string stone;
    public string point;
    
    // 레벨, Exp
    public string level;
    public string exp;
    #endregion

    #region Option
    // Sound
    public float bgmValue;
    public float sfxValue;
    
    // etc.
    public bool shaking;
    public bool autoPowerSaving;
    #endregion
    
}
