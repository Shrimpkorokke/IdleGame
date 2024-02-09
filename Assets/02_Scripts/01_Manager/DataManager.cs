using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class DataManager : MonoSingleton<DataManager>
{
    public PlayerData playerData = new();
    public OptionData optionData = new();

    private string path;
    protected override void Awake()
    {
        path = Application.persistentDataPath + "/";
        LoadLocalOptionData();
#if UNITY_EDITOR
        AAAA();
#endif
    }

    public void JsontoPlayerData(string data)
    {
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void SaveCloud()
    {
        playerData.level = PlayerManager.I.GetLevel().ToString();
        playerData.exp = PlayerManager.I.GetCurrentExp().ToString();

        playerData.point = PlayerManager.I.GetAbilityPoint().ToString();
        playerData.gold = GoodsManager.I.GetGold().ToString();
        playerData.stone = GoodsManager.I.GetStone().ToString();

        GPGSManager.I.SaveCloud("PickaxeMaster_PlayerData", GetJsonPlayerData(), success => Debug.Log($"{success}"));
    }

    public void SetCloudMetaData()
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
        };
    }

    public void LoadLocalOptionData()
    {
        // 파일이 있으면 불러옴
        if (File.Exists(path + "PickaxeMaster_OptionData"))
        {
            string data = File.ReadAllText(path + "PickaxeMaster_OptionData");
            optionData = JsonUtility.FromJson<OptionData>(data);
        }
        // 파일이 없으면 메타데이터 생성 후 저장
        else
        {
            SetLocalMetaData();
            SaveLocal();
        }
    }

    // pc일 때 구글 로그인이 안돼서 로컬에 저장하는 방식으로 테스트를 진행해야함

    public void AAAA()
    {
         string data = "";
        // 파일이 있으면 불러옴
        if (File.Exists(path + nameof(OptionData)))
        {
            data = File.ReadAllText(path + nameof(OptionData));
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
                level = "1", exp = "100",
                // 옵션
                //bgmValue = 1, sfxValue = 1, shaking = true, autoPowerSaving = true
            };
            data = JsonUtility.ToJson(this.playerData);
            File.WriteAllText(path + nameof(PlayerData), data);
        }
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public string GetJsonPlayerData()
    {
        // Data => Json
        string data = JsonUtility.ToJson(this.playerData);
        return data;
    }

    public void SetLocalMetaData()
    {
        optionData = new()
        {
            // 옵션
            bgmValue = 1,
            sfxValue = 1,
            shaking = true,
            autoPowerSaving = true
        };
    }

    public void SaveLocal()
    {
        // Data => Json
        string data = JsonUtility.ToJson(this.optionData);

        // 저장
        File.WriteAllText(path + "PickaxeMaster_OptionData", data);
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
}

public struct OptionData
{
    #region Option
    // Sound
    public float bgmValue;
    public float sfxValue;

    // etc.
    public bool shaking;
    public bool autoPowerSaving;
    #endregion
}
