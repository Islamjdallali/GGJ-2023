using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DeathProgress : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI maxSpeedText;

    [SerializeField] private ScoreCounter scoreCounterScript;

    [SerializeField] private Image barImage;

    [SerializeField] private float target;
    [SerializeField] private float currentScore;
    [SerializeField] private float gainedScore;
    [SerializeField] private float addedScore;

    [SerializeField] private float accel;

    [SerializeField] private GameObject buttons;

    [SerializeField] private Button[] paletteButtons;
    [SerializeField] private int unlockedPalettes;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartProgressBar)
        {
            currentScore += Time.deltaTime * accel;

            accel += Time.deltaTime * 20;

            if (currentScore > addedScore)
            {
                currentScore = addedScore;
                PlayerPrefs.SetFloat("CurrentScore", currentScore);

                StartCoroutine(ShowButtons());
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

    public void ToggleStartProgressBar()
    {
        isStartProgressBar = true;
    }

    void UnlockPalette()
    {
        unlockedPalettes++;
    }
}
