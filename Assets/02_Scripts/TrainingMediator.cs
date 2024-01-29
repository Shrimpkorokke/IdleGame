using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMediator : MonoBehaviour
{
    [SerializeField, GetComponentInChildren(true)] private List<TrainingButton> growthButtonList;
    
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
