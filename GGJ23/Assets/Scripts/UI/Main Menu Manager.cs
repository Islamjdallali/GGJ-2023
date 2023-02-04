using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        int tutorialDone = PlayerPrefs.GetInt("tutorial", 0);

        if (tutorialDone == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Countdown");
        }
    }

    public void Palette()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
