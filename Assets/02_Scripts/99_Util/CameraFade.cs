using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoSingleton<CameraFade>
{
    public Image fadePanel; // 페이드에 사용될 UI 패널
    public float fadeDuration = 1.0f; // 페이드 인아웃 각각의 지속 시간
    public float holdDuration = 1.0f; // 검은 화면 유지 지속 시간

    private void Start()
    {
        SetShake();
    }

    public void StartFade()
    {
        StopCoroutine(FadeSequence());
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // 페이드 아웃
        yield return StartCoroutine(FadeOut());

        // 홀드
        yield return new WaitForSeconds(holdDuration);

        // 페이드 인
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);
            yield return null;
        }
    }

    public void SetShake()
    {
        CFXR_Effect.GlobalDisableCameraShake = !DataManager.I.optionData.shaking;
    }
}

