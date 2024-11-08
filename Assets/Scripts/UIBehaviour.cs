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

    public static UIBehaviour Instance; // singleton dla �atwego dost�pu do UI
    public GameObject levelCompleteMessage; // UI element dla komunikatu o uko�czeniu poziomu

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

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
   //Updating HP Icons
    private void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            //heartIcons[i].enabled = i < currentHealth;
            heart1.sprite = currentHealth >= 1 ? fullHeart : emptyHeart;
            heart2.sprite = currentHealth >= 2 ? fullHeart : emptyHeart;
            heart3.sprite = currentHealth >= 3 ? fullHeart : emptyHeart;
           
        }
    }

    public void ShowEndLevelMessage()
    {
        // W��cz komunikat o uko�czeniu poziomu
        finishLevel.gameObject.SetActive(true);
        finishLevel.text = "Poziom uko�czony! Przej�cie do kolejnego poziomu...";
    }
}