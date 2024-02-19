using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource musicSource; // 배경음악용 오디오 소스
    public AudioSource hitSource; // 효과음용 소스 컴포넌트

    public AudioClip backgroundMusic; // 배경음
    public AudioClip hitSound; // 몬스터가 타격을 입었을 때 재생할 오디오 클립

    private float hitSoundCooldown = 0.025f; // 타격음 재생 간격 (초)
    private float lastHitTime = -1f; // 마지막 타격음 재생 시간

    private void Start()
    {
        ChangeHitVolume();
        ChangeBGMVolume();
    }

    public void PlayBGM()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true; // 배경음악을 루프 설정
        musicSource.Play();
    }

    public void PlayHitSound()
    {
        if (TimeManager.I.isIdle)
            return;

        if (Time.time - lastHitTime >= hitSoundCooldown)
        {
            hitSource.PlayOneShot(hitSound);
            lastHitTime = Time.time;
        }
    }

    public void ChangeHitVolume()
    {
        hitSource.volume = DataManager.I.optionData.sfxValue;
        Debug.Log($"sfxvolume {DataManager.I.optionData.sfxValue}");
    }

    public void ChangeBGMVolume()
    {
        musicSource.volume = DataManager.I.optionData.bgmValue;
    }
}
