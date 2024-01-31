using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        
    }

    public void OnObjectReturn()
    {
        
    }

    private void OnDisable()
    {
        ObjectPoolManager.I.ReturnToPool("HitEffect", this.gameObject);
    }
}
