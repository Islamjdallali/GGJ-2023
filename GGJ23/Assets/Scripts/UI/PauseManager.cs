using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Animator pauseMenuAnim;
    [SerializeField] private Animator resumeButtonAnim;

    [SerializeField] private GameObject pauseCanvas;

    [SerializeField] private PlayerMovement playerMovementScript;

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
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void TurnOffPause()
    {
        playerMovementScript._isCursorLocked = true;
        pauseCanvas.SetActive(false);
    }

    public void Resume()
    {
        buttons[(int)EButtons.EResume].interactable = false;
        resumeButtonAnim.Play("Pressed");
    }
}
