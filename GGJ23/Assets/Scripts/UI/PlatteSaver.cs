using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatteSaver : MonoBehaviour
{
    public GameObject paletteSaver;

    [SerializeField] private Material mat;
    [SerializeField] private Material uiMat;

    void Awake()
    {
        if (paletteSaver != null && paletteSaver != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            paletteSaver = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }

        mat.SetFloat("_PaletteNumber", PlayerPrefs.GetFloat("PaletteNo",0));
        uiMat.SetFloat("_PaletteNumber", PlayerPrefs.GetFloat("PaletteNo",0));
    }
}
