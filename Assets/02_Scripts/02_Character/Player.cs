using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, GetComponentInChildren] private Weapon weapon;
    [SerializeField, GetComponentInChildren] private Animator ani;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (PlayerManager.I.isReady == true)
            {
                yield return new WaitForSeconds(PlayerManager.I.GetAttSpeed());
                AttackAnim();
            }

            yield return null;
        }
    }

    public void AttackAnim()
    {
        
    }
}
