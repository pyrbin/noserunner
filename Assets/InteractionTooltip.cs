using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionTooltip : MonoBehaviour
{
    public SlimePuppeteer puppeteer;
    public Interactor interactor;

    public RectTransform image;

    TextMeshProUGUI text;

    void Start()
    {
        TryGetComponent<TextMeshProUGUI>(out text);
        text.text = "";
        image.transform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (puppeteer.IsShooting)
        {
            text.text = "";
            image.transform.gameObject.SetActive(false);
            return;
        }

        if (interactor.currInteractable && puppeteer.CurrentSlime != interactor.currInteractable)
        {
            text.text = interactor.currInteractable.InteractionDescription();
            image.transform.gameObject.SetActive(true);
        }
        else if (interactor.currInteractableSwitchSlime && puppeteer.CurrentSlime != interactor.currInteractableSwitchSlime)
        {
            text.text = "Swap slime";
            image.transform.gameObject.SetActive(true);
        }
        else
        {
            text.text = "";
            image.transform.gameObject.SetActive(false);
        }

    }

}
