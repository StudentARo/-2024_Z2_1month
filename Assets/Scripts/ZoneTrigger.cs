using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] public Triggerable[] triggerables;
    [SerializeField] private int triggerUses;
    private int _counter = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        foreach (var triggerable in triggerables)
        {
            triggerable.Trigger();
        }
        _counter++;
        if (_counter >= triggerUses)
        {
            Destroy(gameObject);
        }
    }
}
