using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        PCLoadData();
#endif
    }

    public void JsontoPlayerData(string data, Action callback = null)
    {
        playerData = JsonConvert.DeserializeObject<PlayerData>(data);
        Debug.Log($"!!!! JsonToPlayerData 불림 {playerData.point}");
        callback?.Invoke();
    }

    public void SaveCloud(Action<bool> onCloudSaved = null)
    {
        playerData.level = PlayerManager.I.GetLevel().ToString();
        playerData.exp = PlayerManager.I.GetCurrentExp().ToString();

        playerData.point = PlayerManager.I.GetAbilityPoint().ToString();
        playerData.gold = GoodsManager.I.GetGold().ToString();
        playerData.stone = GoodsManager.I.GetStone().ToString();
        playerData.stage = StageManager.I.GetCurrnetStage();
        foreach (var item in PlayerManager.I.trainingSkillLevelDic)
        {
            playerData.trainingLevelDic[item.Key] = item.Value;
        }

        foreach (var item in PlayerManager.I.abilitySkillLevelDic)
        {
            playerData.abilityLevelDic[item.Key] = item.Value;
        }

        GPGSManager.I.SaveCloud("PickaxeIdle_PlayerData", GetJsonPlayerData(), onCloudSaved);
    }

    public void SetCloudMetaData()
    {
        Dictionary<int, int> trainingLevel = new();
        Dictionary<int, int> abilityLevel = new();

        foreach (var VARIABLE in DefaultTable.Training.GetList())
        {
            trainingLevel[VARIABLE.TID] = 0;
        }

        foreach (var VARIABLE in DefaultTable.Ability.GetList())
        {
            abilityLevel[VARIABLE.TID] = 0;
        }

        playerData = new()
        {
            nickName = "Test",
            // 무기
            ownedWeapons = new(){0}, currentWeapon = 0,
            // 재화, 포인트
            gold = "0", stone = "0", point = "0",
            //레벨, exp
            level = "1", exp = "100",
            // Training Level
            trainingLevelDic = trainingLevel,
            abilityLevelDic = abilityLevel,

            // 스테이지
            stage = 1

        };
    }

    public void LoadLocalOptionData()
    {
        // 파일이 있으면 불러옴
        if (File.Exists(path + "PickaxeIdle_OptionData"))
        {
            string data = File.ReadAllText(path + "PickaxeIdle_OptionData");
            optionData = JsonUtility.FromJson<OptionData>(data);
        }
        // 파일이 없으면 메타데이터 생성 후 저장
        else
        {
            SetLocalMetaData();
            SaveLocalOptionData();
        }
    }

    // pc일 때 구글 로그인이 안돼서 로컬에 저장하는 방식으로 테스트를 진행해야함

    public void PCLoadData()
    {
         string data = "";
        // 파일이 있으면 불러옴
        if (File.Exists(path + "PickaxeIdle_PlayerData"))
        {
            data = File.ReadAllText(path + "PickaxeIdle_PlayerData");
        }
        // 파일이 없으면 메타데이터 생성 후 저장
        else
        {
            Dictionary<int, int> trainingLevel = new();
            Dictionary<int, int> abilityLevel = new();

            foreach (var VARIABLE in DefaultTable.Training.GetList())
            {
                trainingLevel[VARIABLE.TID] = 0;
            }

            foreach (var VARIABLE in DefaultTable.Ability.GetList())
            {
                abilityLevel[VARIABLE.TID] = 0;
            }

            playerData = new()
            {
                nickName = "Test",
                // 무기
                ownedWeapons = new() { 0 }, currentWeapon = 0,
                // 재화, 포인트
                gold = "0", stone = "0", point = "0",
                //레벨, exp
                level = "1", exp = "100",
                // Training Level
                trainingLevelDic = new(trainingLevel),
                abilityLevelDic = new(abilityLevel),
                // 스테이지
                stage = 1
            };

            //data = JsonUtility.ToJson(this.playerData);
            data = JsonConvert.SerializeObject(this.playerData);
            File.WriteAllText(path + "PickaxeIdle_PlayerData", data);
        }
        //playerData = JsonUtility.FromJson<PlayerData>(data);
        playerData = JsonConvert.DeserializeObject<PlayerData>(data);
    }

    public string GetJsonPlayerData()
    {
        // Data => Json
        string data = JsonConvert.SerializeObject(this.playerData);
        return data;
    }

    public void SetLocalMetaData()
    {
        optionData = new()
        {
            // 옵션
            bgmValue = 0.2f,
            sfxValue = 0.2f,
            shaking = true,
            autoPowerSaving = true,
            isDoubleSpeed = false,
        };
    }

    public void SaveLocalOptionData()
    {
        // Data => Json
        string data = JsonConvert.SerializeObject(this.optionData);

        // 저장
        File.WriteAllText(path + "PickaxeIdle_OptionData", data);
    }

    public void SaveLocalPlayerData()
    {
        playerData.level = PlayerManager.I.GetLevel().ToString();
        playerData.exp = PlayerManager.I.GetCurrentExp().ToString();

        playerData.point = PlayerManager.I.GetAbilityPoint().ToString();
        playerData.gold = GoodsManager.I.GetGold().ToString();
        playerData.stone = GoodsManager.I.GetStone().ToString();
        playerData.stage = StageManager.I.GetCurrnetStage();

        foreach (var item in PlayerManager.I.trainingSkillLevelDic)
        {
            playerData.trainingLevelDic[item.Key] = item.Value;
        }

        foreach (var item in PlayerManager.I.abilitySkillLevelDic)
        {
            playerData.abilityLevelDic[item.Key] = item.Value;
        }


        // Data => Json
        string data = JsonConvert.SerializeObject(this.playerData);

        // 저장
        File.WriteAllText(path + "PickaxeIdle_PlayerData", data);
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

    // 훈련 레벨
    public Dictionary<int, int> trainingLevelDic;
    public Dictionary<int, int> abilityLevelDic;

    // stage
    public int stage;
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

    // 배속
    public bool isDoubleSpeed;
    #endregion
}
