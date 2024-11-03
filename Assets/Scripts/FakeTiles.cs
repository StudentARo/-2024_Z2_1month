using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FakeTiles : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EaseInOutWall(Color.clear));
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EaseInOutWall(Color.white));
        }
    }
    
    private IEnumerator EaseInOutWall(Color color)
    {
        float animTime = 0.0f;
        while (tilemap.color != color)
        {
            animTime += 0.05f;
            tilemap.color = Color.Lerp(tilemap.color, color, Mathf.Min(animTime, 1));
            yield return new WaitForEndOfFrame();
        }
    }
}
