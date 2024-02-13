using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource HoverSound;

    private void Start()
    {
        HoverSound = GameObject.Find("HoverButtonSound").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HoverSound != null)
        {
            HoverSound.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
