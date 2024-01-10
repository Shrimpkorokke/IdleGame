using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    [SerializeField, GetComponentInChildren] private Weapon weapon;
    [SerializeField, GetComponentInChildren] private Animator ani;
    
    public int basePower;
    public float baseSpeed;
    [HideInInspector] public float baseCriRate;
    [HideInInspector] public float baseCriDmg;
    [HideInInspector] public float dmgPower;
    void Start()
    {
        StartCoroutine(Attack());
        DefaultTable.Training.GetList();
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (PlayerManager.I.isReady == true)
            {
                yield return new WaitForSeconds(GetAttSpeed());
                AttackAnim();
            }

            yield return null;
        }
    }

    public void AttackAnim()
    {
        print("AAAAAAAA");
        //ani.Play("Pickaxe_Attack");
        //ani.SetTrigger("onAttack");
        ani.SetBool("isAttack", true);
        
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public float GetAttSpeed()
    {
        var a = DefaultTable.Training.GetList().Find(x => x.TrainingGrade == TrainingGrade.Normal &&
                                                  x.TrainingType == TrainingType.AttSpeed);
        print(baseSpeed - a.AdditionalVal * PlayerManager.I.attSpeedLv);
        return 1f - a.AdditionalVal;
    }
}