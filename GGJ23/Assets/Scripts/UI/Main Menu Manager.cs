using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("level");
    }

    public void Palette()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
