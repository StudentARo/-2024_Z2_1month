using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SilverCoin;
using Collectibles;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using System;
using Player;
using System.Data.Common;


public class UIBehaviour : MonoBehaviour
{
    public Text scoreText;  // Referencja do pola tekstowego UI
    private int initialScore;
    [SerializeField]

    //Hearts fields reference
    public Image heart1;        
    [SerializeField]
    public Image heart2;        
    [SerializeField] 
    public Image heart3;        
    
    //Heart UI Referece
    [SerializeField]
    public Sprite fullHeart;   
    [SerializeField]
    public Sprite emptyHeart;

    //Heart sprites array
    [SerializeField]
    public Image[] heartIcons;

    //Dead
    [SerializeField]
    public Text dead;
    [SerializeField] 
    private Text finishLevel;
    PlayerHealth playerHealth;

    public static UIBehaviour Instance; // singleton dla ³atwego dostêpu do UI
    public GameObject levelCompleteMessage; // UI element dla komunikatu o ukoñczeniu poziomu

    // public String initialDeadText = "";
    //public String deadText = "You have died!";
    //updating score and HP
    private void OnEnable()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        scoreText.text = initialScore.ToString();
        PlayerScore.OnScoreUpdated += UpdateScoreUI;
        PlayerHealth.OnHealthUpdated += UpdateHealthUI;
      //  initialDeadText = dead.ToString();
        dead.gameObject.SetActive(false);
    }
    //Disabling updating hp and score
    private void OnDisable()
    {
        PlayerScore.OnScoreUpdated -= UpdateScoreUI;
        PlayerHealth.OnHealthUpdated -= UpdateHealthUI;
    }
    private void Start()
    {
        //hidding field on game start
        dead.gameObject.SetActive(false);
    }
    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    //Updating HP Icons
    private void UpdateHealthUI(int currentHealth)
    {
        // setting hp icons to actual level
        heart1.sprite = currentHealth >= 1 ? fullHeart : emptyHeart;
        heart2.sprite = currentHealth >= 2 ? fullHeart : emptyHeart;
        heart3.sprite = currentHealth >= 3 ? fullHeart : emptyHeart;

        // if hp == 0 disable HP sprite and show "You are dead"
        if (currentHealth <= 0)
        {
            heart1.sprite = emptyHeart; // if hp == 0 set emptyHeart icon
            dead.gameObject.SetActive(true); // display "You are dead"
            dead.text = "You are dead";
        }
        else
        {
            // disable information when hp is > than 0
            dead.gameObject.SetActive(false);
        }
    }
}