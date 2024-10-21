using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField]
    int lives = 3;
    [SerializeField]
    TextMeshProUGUI livesDisplay;
    
    [SerializeField]
    int coins = 0;

    [SerializeField] 
    TextMeshProUGUI coinsDisplay;

   void Start()
   {
    livesDisplay.text = lives.ToString();
    livesDisplay.text = coins.ToString();
   }
   public void antiDuplicateUI(){
    int playerUINumber = FindObjectsOfType<UIBehaviour>().Length;
    if(playerUINumber <1){
        DontDestroyOnLoad(gameObject);
    }else{
        Destroy(gameObject);
    }
   }

   public void Death(){
    if(lives > 1){
        lives--;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        livesDisplay.text = coins.ToString();
    }else{
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
   }
   public void addCoins(int coinsAdd)
   {
    coins +=coinsAdd;
    coinsDisplay.text = coins.ToString();
   }
}