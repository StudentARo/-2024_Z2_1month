using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorTrigger : MonoBehaviour
{
    [SerializeField] private float trapdoorOpenDelay;
    [SerializeField] private float trapdoorCloseDelay;
    private HingeJoint2D _hingeJoint2D;
    
    void Start()
    {
        _hingeJoint2D = GetComponent <HingeJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(OpenAndCloseTrapdoorAfterDelay());
        }
    }

    private IEnumerator OpenAndCloseTrapdoorAfterDelay()
    {
        yield return new WaitForSeconds(trapdoorOpenDelay);
        _hingeJoint2D.useMotor = false;
        yield return new WaitForSeconds(trapdoorCloseDelay);
        _hingeJoint2D.useMotor = true;
    }
}
