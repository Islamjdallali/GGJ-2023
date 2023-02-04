using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathwall : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeTillSpeedup;
    [SerializeField] private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeTillSpeedup;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxSpeed)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                speed += 50;
                timer = timeTillSpeedup;
            }
        }

        gameObject.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    }
}
