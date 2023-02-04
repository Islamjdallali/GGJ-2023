using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerCollision playerCollisionScript;
    [SerializeField] private Rigidbody playerRb;

    public float distance;
    public float maxSpeed;
    public float timer;

    private void Start()
    {
        distance = 0;
        maxSpeed = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCollisionScript.isDead)
        {
            timer += Time.deltaTime;

            if (playerTransform.position.z > distance)
            {
                distance = playerTransform.position.z;
            }

            if (playerRb.velocity.magnitude > maxSpeed)
            {
                maxSpeed = playerRb.velocity.magnitude;
            }

            maxSpeed = playerRb.velocity.magnitude;

            scoreText.text = "Score : " + playerTransform.position.z.ToString("F2");
        }
    }
}
