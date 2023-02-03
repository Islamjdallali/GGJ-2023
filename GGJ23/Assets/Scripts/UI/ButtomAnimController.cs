using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomAnimController : MonoBehaviour
{
    [SerializeField] private Animator pauseMenuAnim;

    public void UnPause()
    {
        pauseMenuAnim.Play("PauseOut");
    }
}
