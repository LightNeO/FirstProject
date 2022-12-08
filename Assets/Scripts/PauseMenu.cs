using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButtonFirst;
    [SerializeField] private Button _exitButtonSecond;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private bool isOpened = false;
    void Start()
    {
        _continueButton.onClick.AddListener(ContinueGame);
        _mainMenuButton.onClick.AddListener(OpenMainMenu);
        _exitButtonFirst.onClick.AddListener(ExitGame);
        _exitButtonSecond.onClick.AddListener(ExitGame);
        _pauseButton.onClick.AddListener(PauseGame);
    }

    private void PauseGame()
    {
        if (!isOpened)
        {
            Time.timeScale = 0;
            isOpened = true;
            pauseMenu.SetActive(true);
        }
        else if (isOpened)
        {
            Time.timeScale = 1;
            isOpened = false;
            pauseMenu.SetActive(false);
        }
        
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
}
