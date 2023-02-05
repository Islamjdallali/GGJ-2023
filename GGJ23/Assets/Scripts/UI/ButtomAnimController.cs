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
        pauseMenuAnim.Play("PauseOut");
    }

    public void ShowPaletteMenu()
    {
        if (paletteCanvas != null)
        {
            paletteCanvas.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
