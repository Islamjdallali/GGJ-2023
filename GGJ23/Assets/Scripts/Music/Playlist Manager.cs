using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    [SerializeField] private int randomNo;

    [SerializeField] private GameObject[] songs;

    // Start is called before the first frame update
    void Start()
    {
        randomNo = Random.Range(0, songs.Length);

        Instantiate(songs[randomNo]);
    }
}
