using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Material uiMat;

    private Animator paletteManagerAnim;

    [SerializeField] private int numberOfPalettes;

    [SerializeField] private Button paletteMenuButton;

    private void Start()
    {
        paletteManagerAnim = GetComponent<Animator>();
    }


    public void PalettePicked(int paletteNo)
    {
        mat.SetFloat("_PaletteNumber", (float)paletteNo / numberOfPalettes);
        uiMat.SetFloat("_PaletteNumber", (float)paletteNo / numberOfPalettes);
    }

    public void Back()
    {
        paletteManagerAnim.Play("PaletteOut");
        paletteMenuButton.interactable = true;
    }

    public void DisableManager()
    {
        gameObject.SetActive(false);
    }
}
