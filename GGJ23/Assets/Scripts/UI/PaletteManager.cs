using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    [SerializeField] private Material mat;

    [SerializeField] private int numberOfPalettes;


    public void Palette1()
    {
        mat.SetFloat("_PaletteNumber", (numberOfPalettes - 1) / 1);
    }
}
