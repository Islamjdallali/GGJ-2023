using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustingVolume : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = (float)PlayerPrefs.GetInt("volume", 10) / 10;
    }
}
