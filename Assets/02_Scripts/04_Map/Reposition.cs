using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void LateUpdate()
    {
        if(transform.position.x > -27.81f)
            return;
        
        transform.Translate(46.35f, 0, 0, Space.Self);
    }
}
