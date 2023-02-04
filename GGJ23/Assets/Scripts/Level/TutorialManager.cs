using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject fadeOutImage;

    private void Start()
    {
        fadeOutImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fadeOutImage.SetActive(true);
            PlayerPrefs.SetInt("tutorial", 1);
        }
    }
}
