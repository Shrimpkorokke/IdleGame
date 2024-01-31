using UnityEngine;

public class CameraShake : MonoSingleton<CameraShake>
{
    // 셰이크 지속 시간
    public float shakeDuration = 0.5f;

    // 셰이크 강도
    public float shakeMagnitude = 0.7f;

    // 셰이크가 줄어드는 속도
    public float dampingSpeed = 1.0f;

    Vector3 initialPosition;
    float currentShakeDuration;

    void Awake()
    {
        if (transform == null)
        {
            Debug.LogError("Transform is null");
        }
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    // 이 함수를 호출하여 셰이크 효과를 시작합니다
    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
    }
}
