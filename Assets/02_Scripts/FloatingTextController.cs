using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoSingleton<FloatingTextController>
{
    [SerializeField] private FloatingText floatingText;
    [SerializeField] private GameObject canvas;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        canvas = GameObject.Find("Canvas");
        
        if(!floatingText)
            floatingText = Resources.Load<FloatingText>("DamageText");
    }
    
    public void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(floatingText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
