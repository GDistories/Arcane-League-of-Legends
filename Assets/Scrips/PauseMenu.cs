using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObj;
    public GameObject DieMenuObj;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuObj.SetActive(true);
            Time.timeScale = 0;
        }

        if (!GameController.isGameAlive)
        {
            DieMenuObj.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ResumeGame()
    {
        PauseMenuObj.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        GameController.isGameAlive = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
