using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CameraTransitionButton : MonoBehaviour
{
    public void Start()
    {
        hovered = false;
        material.color = new Color(baseMatColor.r, baseMatColor.g, baseMatColor.b, baseMatColor.a / 8f);
    }

    public void TryMouseEnter()
    {
        if (hovered) return;
        hovered = true;
        material.color = baseMatColor;
    }

    public void TryMouseExit()
    {
        if (!hovered) return;
        hovered = false;
        material.color = new Color(baseMatColor.r, baseMatColor.g, baseMatColor.b, baseMatColor.a / 8f);
    }

    public void Click()
    {
        PlayerController.instance.TransitionCamera(cam);
    }

    public int cam;

    public Material material;
    public Color baseMatColor;

    [HideInInspector]
    public bool hovered;
}
