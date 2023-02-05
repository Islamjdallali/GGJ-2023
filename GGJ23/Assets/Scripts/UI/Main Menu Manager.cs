using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject paletteCanvas;

    [SerializeField] private Animator playGameAnim;
    [SerializeField] private Animator paletteAnim;
    [SerializeField] private Animator quitGameAnim;

    public void PlayGame()
    {
        playGameAnim.Play("PlayButtonPressed");
    }

    public void ShowPaletteMenu()
    {
        paletteAnim.Play("PaletteButtonPressed");
    }

    public void QuitGame()
    {
        quitGameAnim.Play("QuitButtonPressed");
    }
}
