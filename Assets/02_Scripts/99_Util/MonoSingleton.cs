using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T I
    {
        get
        {
            if (_instance == null)
            {
                // Scene 내에 이미 인스턴스가 있는지 확인
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // 인스턴스가 없다면 새로 생성
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = typeof(T).Name + " (Singleton)";

                    // 선택적: 씬 전환 시 파괴되지 않도록 설정
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
                Destroy(gameObject);
        }
    }
}
