using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
 
    [SerializeField]
    public Button backButton;
    private void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        Debug.Log("Exit Game");
        SceneManager.LoadScene("MainMenu");
    }
}
