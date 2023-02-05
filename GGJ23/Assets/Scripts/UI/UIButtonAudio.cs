using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UIButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource highlightSFX;
    [SerializeField] private AudioSource selectSFX;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().IsInteractable())
        {
            highlightSFX.Play();
        }
    }

    public void PlaySelectedSFX()
    {
        selectSFX.Play();
    }
}
