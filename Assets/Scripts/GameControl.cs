using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void goHome()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void reStartGame()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("GamePlay");
    }


}
