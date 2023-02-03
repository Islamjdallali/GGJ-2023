using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerCollision playerCollisionScript;

    // Update is called once per frame
    void Update()
    {
        if (playerCollisionScript.isDead)
        {
            scoreText.text = "Score : " + playerTransform.position.z.ToString("F2");
        }
    }
}
