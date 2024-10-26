using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D platformBoxCollider2D;
    [SerializeField] private PolygonCollider2D solidPlatformPolygonCollider2D;
    [SerializeField] private PolygonCollider2D nonSolidPlatformPolygonCollider2D;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("Pressed down");
            platformBoxCollider2D.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.IsTouching(solidPlatformPolygonCollider2D))
        {
            platformBoxCollider2D.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.IsTouching(nonSolidPlatformPolygonCollider2D))
        {
            platformBoxCollider2D.enabled = false;
        }
    }
}
