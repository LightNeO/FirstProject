 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text topScoreText;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(CloseGame);
        topScoreText.text = PlayerPrefs.GetInt("TopScore", 0).ToString();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    private void CloseGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
