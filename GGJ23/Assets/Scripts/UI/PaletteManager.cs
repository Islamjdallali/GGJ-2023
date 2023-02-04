using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    [SerializeField] private Material mat;

    [SerializeField] private int numberOfPalettes;


    public void PalettePicked(int paletteNo)
    {
        mat.SetFloat("_PaletteNumber", (float)paletteNo / numberOfPalettes);

        Debug.Log(paletteNo / numberOfPalettes);
    }
}
