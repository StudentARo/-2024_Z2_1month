using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public Button continueButton;
    [SerializeField]
    public Button newGameButton;
    [SerializeField]
    public Button settingsButton;
    [SerializeField]
    public Button highScoreButton;
    [SerializeField]
    public Button creditsButton;
    [SerializeField]
    public Button exitButton;

    private void Start()
    {
        // Assign functions to button clicks
        continueButton.onClick.AddListener(OnContinueClicked);
        newGameButton.onClick.AddListener(OnNewGameClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        highScoreButton.onClick.AddListener(OnHighScoreClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    // Load last checkpoint
    private void OnContinueClicked()
    {
        Debug.Log("Continue Game");
       
    }

    //Level 1 scene
    private void OnNewGameClicked()
    {
        Debug.Log("New Game");
        SceneManager.LoadScene("Level_1");
    }

    // Settings Scene
    private void OnSettingsClicked()
    {
        Debug.Log("Open Settings");
        SceneManager.LoadScene("Settings");
    }

    // High Score scene
    private void OnHighScoreClicked()
    {
        Debug.Log("Open High Scores");
        SceneManager.LoadScene("HighScore");
    }

    // Credits Scene
    private void OnCreditsClicked()
    {
        Debug.Log("Open Credits");
        SceneManager.LoadScene("Credits");
    }

    //Exit game
    private void OnExitClicked()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}