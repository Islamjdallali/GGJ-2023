using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtomAnimController : MonoBehaviour
{
    [SerializeField] private Animator pauseMenuAnim;
    [SerializeField] private Button pauseButton;

    public void UnPause()
    {
        pauseMenuAnim.Play("PauseOut");
    }
}
