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

    //Hearths fields reference
    public Image heart1;        
    [SerializeField]
    public Image heart2;        
    [SerializeField] 
    public Image heart3;        
    
    //Hearth UI Referece
    [SerializeField]
    public Sprite fullHeart;   
    [SerializeField]
    public Sprite emptyHeart;

    //Heart sprites array
    [SerializeField]
    public Image[] heartIcons;

    //updating score and HP
    private void OnEnable()
    {
        scoreText.text = initialScore.ToString();
        PlayerScore.OnScoreUpdated += UpdateScoreUI;
        PlayerHealth.OnHealthUpdated += UpdateHealthUI;
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
        // Aktualizacja ikon serc na podstawie zdrowia
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = i < currentHealth;  
            heart1.sprite = currentHealth >= 1 ? fullHeart : emptyHeart;
            heart2.sprite = currentHealth >= 2 ? fullHeart : emptyHeart;
            heart3.sprite = currentHealth >= 3 ? fullHeart : emptyHeart;
        }
       
    }
}