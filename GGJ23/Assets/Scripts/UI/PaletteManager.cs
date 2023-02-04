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

    [SerializeField] private Button[] paletteButtons;

    [SerializeField] private int unlockedPalettes;

    private void Start()
    {
        paletteManagerAnim = GetComponent<Animator>();

        unlockedPalettes = PlayerPrefs.GetInt("UnlockedPalettes", 1);

        for (int i = 0; i < paletteButtons.Length; i++)
        {
            paletteButtons[i].interactable = false;
        }

        for (int i = 0; i < unlockedPalettes; i++)
        {
            paletteButtons[i].interactable = true;
        }
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
