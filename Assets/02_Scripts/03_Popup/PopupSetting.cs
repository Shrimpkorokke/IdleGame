using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : Popup
{
    // 나가기 버튼
    [SerializeField, GetComponentInChildrenName("Btn_Exit")] private Button btnExit;
    [SerializeField, GetComponentInChildrenName("Btn_BG")] private Button btnBG;
    
    // 슬라이더
    [SerializeField, GetComponentInChildrenName("Slider_BGM")] private Slider sliderBgm;
    [SerializeField, GetComponentInChildrenName("Slider_Sfx")] private Slider sliderSfx;
    [SerializeField, GetComponentInChildrenName("Slider_Shaking")] private Slider sliderShaking;
    [SerializeField, GetComponentInChildrenName("Slider_PowerSaving")] private Slider SliderPowerSaving;

    // On,Off 버튼
    [SerializeField, GetComponentInChildrenName("Btn_Shaking")] private Button btnShaking;
    [SerializeField, GetComponentInChildrenName("Btn_PowerSaving")] private Button btnPowerSaving;
    
    // 구글 로그인 버튼
    [SerializeField, GetComponentInChildrenName("Btn_Google")] private Button btngoogle;
    
    private float bgmValue;
    private float sfxValue;

    private float shaking;
    private float autoPowerSaving;
    private int hasSet;
    protected override void Awake()
    {
        btnExit.onClick.AddListener(() => Close());
        btnBG.onClick.AddListener(() => Close());
        btnShaking.onClick.AddListener(() =>
        {
            shaking = shaking < 1 ? 1 : 0;
            sliderShaking.value = shaking;
            Debug.Log("shaking 버튼 눌림");
        });
        
        btnPowerSaving.onClick.AddListener(() =>
        {
            autoPowerSaving = autoPowerSaving < 1 ? 1 : 0;
            SliderPowerSaving.value = autoPowerSaving;
            Debug.Log("power 버튼 눌림");
        });
    }
    
    private void LoadData()
    {
        // 설정값이 없을 경우
        if (PlayerPrefs.HasKey("hasSet") == false)
        {
            Debug.Log(" 값이 없음");
            bgmValue = 1f;
            sfxValue = 1f;
            shaking = 1;
            autoPowerSaving = 1;
            SaveData();
        }
        // 설정값이 있을 경우
        else
        {
            Debug.Log(" 값이 있음");
            // float 배경음, 효과음
            bgmValue = PlayerPrefs.GetFloat("bgmValue");
            sfxValue = PlayerPrefs.GetFloat("sfxValue");
        
            // int(bool 대신) 화면 흔들림, 자동 절전모드
            shaking = PlayerPrefs.GetFloat("shaking");
            autoPowerSaving = PlayerPrefs.GetFloat("powerSaving");
        }
        
        sliderBgm.value = bgmValue;
        sliderSfx.value = sfxValue;
        sliderShaking.value = shaking;
        SliderPowerSaving.value = autoPowerSaving;
    }

    private void SaveData()
    {
        bool set = hasSet == 1 ? true : false;

        if (set == true)
        {
            bgmValue = sliderBgm.value;
            sfxValue = sliderSfx.value;
        }
        else
            hasSet = 1;
        
        PlayerPrefs.SetInt("hasSet", hasSet);
        PlayerPrefs.SetFloat("bgmValue", bgmValue);
        PlayerPrefs.SetFloat("sfxValue", sfxValue);
        
        PlayerPrefs.SetFloat("shaking", shaking);
        PlayerPrefs.SetFloat("powerSaving", autoPowerSaving);
        PlayerPrefs.Save();
    }

    
    
    public override void Open()
    {
        LoadData();
        base.Open();
    }

    public override void Close()
    {
        SaveData();
        base.Close();
    }
}
