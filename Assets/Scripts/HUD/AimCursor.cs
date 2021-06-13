using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimCursor : MonoBehaviour
{

    public Color color;
    public Color hoverColor;
    public Interactor interactor;
    public SlimePuppeteer slimePuppeteer;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = color;
    }

    void Update()
    {
        if (slimePuppeteer.IsShooting)
        {
            image.color = color;
            return;
        }

        if (interactor.currInteractableSwitchSlime || interactor.currInteractable)
        {
            image.color = hoverColor;
        }
        else
        {
            image.color = color;
        }
    }
}
