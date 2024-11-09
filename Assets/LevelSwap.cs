using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelSwap : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D other)
    {
       //checking Player Tag
        if (other.CompareTag("Player"))
        {
            // get scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // load next scene
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}