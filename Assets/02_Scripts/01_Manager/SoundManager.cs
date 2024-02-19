using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource musicSource; // ������ǿ� ����� �ҽ�
    public AudioSource hitSource; // ȿ������ �ҽ� ������Ʈ

    public AudioClip backgroundMusic; // �����
    public AudioClip hitSound; // ���Ͱ� Ÿ���� �Ծ��� �� ����� ����� Ŭ��

    private float hitSoundCooldown = 0.025f; // Ÿ���� ��� ���� (��)
    private float lastHitTime = -1f; // ������ Ÿ���� ��� �ð�

    private void Start()
    {
        ChangeHitVolume();
        ChangeBGMVolume();
    }

    public void PlayBGM()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true; // ��������� ���� ����
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
