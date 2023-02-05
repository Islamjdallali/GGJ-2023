using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CountDownManager : MonoBehaviour
{
    [SerializeField] private AudioSource countDownSFX;
    [SerializeField] private AudioSource startSFX;

    public void StartGame()
    {
        SceneManager.LoadScene("level");
    }

    public void CountDownSFX()
    {
        countDownSFX.Play();
    }

    public void StartSFX()
    {
        startSFX.Play();
    }
}
