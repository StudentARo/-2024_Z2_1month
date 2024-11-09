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
           // SceneManager.LoadScene(currentSceneIndex + 1);
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            if (currentSceneIndex + 1 < sceneCount)
            {
                Invoke("LoadNextScene", 2f); // reload scene delay
            }
            else
            {
                Debug.Log("Nie ma nastêpnej sceny!");
            }
        }
    }

    // Funkcja do za³adowania nastêpnej sceny
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Getting button R
        {
            RestartScene(); // Restart scene
        }
    }
    private void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // load actual scene
    }
}