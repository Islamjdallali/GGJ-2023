using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimController : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    public void TurnOffPause()
    {
        pauseCanvas.SetActive(false);
    }
}
