using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimCursor : MonoBehaviour
{

    public Color color;
    public Color hoverColor;
    public Interactor interactor;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = color;

        interactor.Found += (interactable) =>
        {
            image.color = hoverColor;
        };

        interactor.Lost += (interactable) =>
        {
            image.color = color;
        };
    }
}
