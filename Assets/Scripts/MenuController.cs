using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        Application.LoadLevel("GamePlay");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
