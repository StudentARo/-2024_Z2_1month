using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBarrel : BarrelMovement
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
