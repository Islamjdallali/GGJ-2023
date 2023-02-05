using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DeathProgress : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI maxSpeedText;

    [SerializeField] private ScoreCounter scoreCounterScript;

    [SerializeField] private Image barImage;

    [Header("Stats")]
    [SerializeField] private float target;
    [SerializeField] private float currentScore;
    [SerializeField] private float gainedScore;
    [SerializeField] private float addedScore;

    [SerializeField] private float accel;

    [SerializeField] private GameObject buttons;
    [SerializeField] private Animator retryButtonAnim;
    [SerializeField] private Animator quitButtonAnim;

    [Header("SFX")]
    [SerializeField] private AudioSource statSFX;
    [SerializeField] private AudioSource progressbarSFX;
    [SerializeField] private AudioSource unlockPaletteSFX;

    [Header("Unlock Palettes")]
    [SerializeField] private int unlockedPalettes;
    [SerializeField] private GameObject unlockPaletteText;

    private bool isStartProgressBar;

    private void OnEnable()
    {
        isStartProgressBar = false;
        timerText.text = "Time : " + scoreCounterScript.timer.ToString("F2");
        distanceText.text = "Distance : " + scoreCounterScript.distance.ToString("F2");
        maxSpeedText.text = "Max Speed : " + scoreCounterScript.maxSpeed.ToString("F2");

        currentScore = PlayerPrefs.GetFloat("CurrentScore", 0);

        accel = 1;

        barImage.fillAmount = currentScore;

        gainedScore = scoreCounterScript.distance;

        addedScore = currentScore + gainedScore;

        buttons.SetActive(false);
        unlockPaletteText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartProgressBar)
        {
            if (currentScore < addedScore)
            {
                currentScore += Time.deltaTime * accel;
                progressbarSFX.gameObject.SetActive(true);
                progressbarSFX.pitch += Time.deltaTime * 0.1f;
                accel += Time.deltaTime * 100;
            }

            if (currentScore >= addedScore)
            {
                currentScore = addedScore;
                PlayerPrefs.SetFloat("CurrentScore", currentScore);

                StartCoroutine(ShowButtons());
            }

            if (currentScore > target)
            {
                UnlockPalette();
            }

            barImage.fillAmount = currentScore / target;
        }
    }

    IEnumerator ShowButtons()
    {

        yield return new WaitForSeconds(1);

        buttons.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator PlayProgressAudio()
    {
        while(currentScore < addedScore)
        {
            progressbarSFX.Play();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void PlayStatsAudio()
    {
        statSFX.Play();
    }

    public void ToggleStartProgressBar()
    {
        isStartProgressBar = true;
    }

    void UnlockPalette()
    {
        unlockPaletteText.SetActive(true);
        unlockPaletteSFX.Play();

        unlockedPalettes = PlayerPrefs.GetInt("UnlockedPalettes", 1);

        unlockedPalettes++;

        PlayerPrefs.SetInt("UnlockedPalettes", unlockedPalettes);

        PlayerPrefs.SetFloat("CurrentScore", 0);

        currentScore = 0;
        addedScore = 0;
    }

    public void Retry()
    {
        retryButtonAnim.Play("RetryButtonPressed");
    }

    public void MainMenu()
    {
        quitButtonAnim.Play("QuitButtonPressed");
    }
}
