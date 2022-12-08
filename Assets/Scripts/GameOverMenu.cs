using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _gameOverMenu;
    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _mainMenuButton.onClick.AddListener(OpenMainMenu);
        _exitButton.onClick.AddListener(ExitGame);
    }


    private void RestartGame()
    {
        Player.isDead = false;
        Score.scoreValue = 0;
        Time.timeScale = 1;
        _gameOverMenu.SetActive(false);
        SceneManager.LoadScene(1);

    }

    private void OpenMainMenu()
    {
        Player.isDead = false;
        Score.scoreValue = 0;
        Time.timeScale = 1;
        _gameOverMenu.SetActive(false);
        SceneManager.LoadScene(0);

    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
