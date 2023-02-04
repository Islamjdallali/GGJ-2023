using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Animator pauseMenuAnim;
    [SerializeField] private Animator resumeButtonAnim;
    [SerializeField] private Animator paletteButtonAnim;

    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject paletteCanvas;

    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private Vector3 playerVelocity;

    [SerializeField] private Button[] buttons;

    private enum EButtons
    {
        EResume,
        EPalette,
        EOptions,
        EMainMenu
    }

    private void OnEnable()
    {
        Time.timeScale = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        paletteCanvas.SetActive(false);
    }

    public void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void TurnOffPause()
    {
        playerMovementScript.isCursorLocked = true;
        pauseCanvas.SetActive(false);
    }

    public void Resume()
    {
        buttons[(int)EButtons.EResume].interactable = false;
        resumeButtonAnim.Play("Pressed");
    }

    public void Palette()
    {
        buttons[(int)EButtons.EPalette].interactable = false;
        paletteButtonAnim.Play("PressedPalette");
    }

}
