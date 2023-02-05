using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScoreManager : MonoBehaviour
{
    public GameObject resetScoreGO;

    void Awake()
    {
        if (resetScoreGO != null && resetScoreGO != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            resetScoreGO = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
        {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
