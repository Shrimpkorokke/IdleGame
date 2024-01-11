using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField, GetComponentInChildren(true)] private List<GrowthButton> growthButtonList;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < growthButtonList.Count; i++)
        {
            growthButtonList[i].Init();
        }
    }
}
