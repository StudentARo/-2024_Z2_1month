using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelSwap : MonoBehaviour
{
    public UIBehaviour uIBehaviour;

    private void OnTriggerEnter2D(Collider2D other)
    {
       //checking Player Tag
        if (other.CompareTag("Player"))
        {
            if (uIBehaviour == null)
            {
                return;
            }
            uIBehaviour.ShowLevelCompletedMessage();
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
                Debug.Log("Scene does not exist");
            }
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        uIBehaviour.HideLevelCompletedMessage();
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