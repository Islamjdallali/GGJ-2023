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
    [SerializeField] private float addedScore;

    [SerializeField] private float accel;

    private bool isStartProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        isStartProgressBar = false;
        timerText.text = "Time : " + scoreCounterScript.timer.ToString("F2");
        distanceText.text = "Distance : " + scoreCounterScript.distance.ToString("F2");
        maxSpeedText.text = "Max Speed : " + scoreCounterScript.maxSpeed.ToString("F2");

        currentScore = PlayerPrefs.GetFloat("CurrentScore", 0);

        accel = 1;

        barImage.fillAmount = currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartProgressBar)
        {
            currentScore += Time.deltaTime * accel;

            accel += Time.deltaTime * 20;

            barImage.fillAmount = currentScore / target;
        }
    }

    public void ToggleStartProgressBar()
    {
        isStartProgressBar = true;
    }
}
