using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtomAnimController : MonoBehaviour
{
    [SerializeField] private Animator pauseMenuAnim;

    [SerializeField] private GameObject paletteCanvas;

    public void UnPause()
    {
        if (pauseMenuAnim != null)
        {
            pauseMenuAnim.Play("PauseOut");
        }
    }

    public void ShowPaletteMenu()
    {
        if (paletteCanvas != null)
        {
            paletteCanvas.SetActive(true);
        }
    }

    public void StartGame()
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

    public void Retry()
    {
        SceneManager.LoadScene("Countdown");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
