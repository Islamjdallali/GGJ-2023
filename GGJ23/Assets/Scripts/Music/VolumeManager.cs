using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public GameObject volumeManager;

    [SerializeField] private int volume = 5;

    void Awake()
    {
        if (volumeManager != null && volumeManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            volumeManager = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals) && volume < 10
            || Input.GetKeyDown(KeyCode.Plus) && volume < 10)
        {
            volume += 1;
            PlayerPrefs.SetInt("volume", volume);
        }
        else if (Input.GetKeyDown(KeyCode.Minus) && volume > 0)
        {
            volume -= 1;
            PlayerPrefs.SetInt("volume", volume);
        }
    }
}
