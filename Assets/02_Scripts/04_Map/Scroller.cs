using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public int count;
    public float speedRate;
    void Start()
    {
        count = transform.childCount;
    }

    void Update()
    {
        if (PlayerManager.I.isAttack)
            return;
        
        transform.Translate(speedRate * Time.deltaTime * -1f , 0, 0);
    }
}
