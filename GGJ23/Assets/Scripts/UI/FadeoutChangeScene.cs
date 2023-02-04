using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FadeoutChangeScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("level");
    }
}
