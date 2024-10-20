using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableSpawner : Triggerable
{
    [SerializeField] private GameObject spawnedPrefab;

    public override void Trigger()
    {
        Instantiate(spawnedPrefab, transform.position, transform.rotation);
    }
}
