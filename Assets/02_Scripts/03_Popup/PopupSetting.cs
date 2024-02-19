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
    
    protected override void Awake()
    {
        btnExit.onClick.AddListener(() => Close());
        btnBG.onClick.AddListener(() => Close());
        btnShaking.onClick.AddListener(() =>
        {
            DataManager.I.optionData.shaking = !DataManager.I.optionData.shaking;
            sliderShaking.value = DataManager.I.optionData.shaking == true ? 1 : 0;
            Debug.Log("shaking 버튼 눌림");
        });
        
        btnPowerSaving.onClick.AddListener(() =>
        {
            DataManager.I.optionData.autoPowerSaving = !DataManager.I.optionData.autoPowerSaving;
            SliderPowerSaving.value = DataManager.I.optionData.autoPowerSaving == true ? 1 : 0;
            Debug.Log("power 버튼 눌림");
        });
    }
    
    private void LoadData()
    {
        sliderBgm.value = DataManager.I.optionData.bgmValue;
        sliderSfx.value = DataManager.I.optionData.sfxValue * 5;
        sliderShaking.value = DataManager.I.optionData.shaking == true ? 1 : 0;
        SliderPowerSaving.value = DataManager.I.optionData.autoPowerSaving == true ? 1 : 0;
    }

    private void SaveData()
    {
        DataManager.I.optionData.bgmValue = sliderBgm.value;
        DataManager.I.optionData.sfxValue = sliderSfx.value / 5;
        DataManager.I.optionData.shaking = sliderShaking.value < 1 ? false : true;
        DataManager.I.optionData.autoPowerSaving = SliderPowerSaving.value < 1 ? false : true;

        DataManager.I.SaveLocalOptionData();

        SoundManager.I.ChangeHitVolume();
        SoundManager.I.ChangeBGMVolume();
        CameraFade.I.SetShake();
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
