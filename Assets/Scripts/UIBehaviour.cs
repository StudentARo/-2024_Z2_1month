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
    int hp;

    [SerializeField]
    int lives;

    [SerializeField]
    int keys;

    [SerializeField]
    int coins = 0;

    [SerializeField] 
    TextMeshProUGUI coinsDisplay;
    [SerializeField]
    TextMeshProUGUI keysDisplay;

    public Image[] hpImages;
    public Sprite hpActive;
    public Sprite hpDisable;

    void Start()
   {
        coinsDisplay.text = coins.ToString();
        keysDisplay.text = keys.ToString();
   }
    private void Update()
    {
        LivesCountCheck();
        for (int i = 0; i < hpImages.Length; i++)
        {
            disableHeartIcon(i);
            useSpriteIcon(i);
        }
    }

    private void useSpriteIcon(int i)
    {
        if (i < hp)
        {
            hpImages[i].sprite = hpActive;
        }
        else
        {
            hpImages[i].sprite = hpDisable;
        }
    }

    private void disableHeartIcon(int i)
    {
        if (i < lives)
        {
            hpImages[i].enabled = true;
        }
        else
        {
            hpImages[i].enabled = false;
        }
    }

    private void LivesCountCheck()
    {
        if (hp > lives)
        {
            hp = lives;
        }
    }

    public void addCoins(int coinsAdd, Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            coinsAdd += coinsAdd;
        }
           
        coinsDisplay.text = coinsAdd.ToString();
    }
    public void addKeys(int keysAdd)
    {
        keys += keysAdd;
        keysDisplay.text = keys.ToString();
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
        if(lives > 1)
        {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        }
        else
        {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        }
   }
}