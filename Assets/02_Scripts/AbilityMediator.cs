using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AbilityMediator : MonoBehaviour
{
    [SerializeField, GetComponentInChildren(true)] private List<AbilityButton> abilityButtonList;
    
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < abilityButtonList.Count; i++)
        {
            abilityButtonList[i].Init();
        }
    }
}
