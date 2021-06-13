using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionTooltip : MonoBehaviour
{
    public Interactor interactor;
    TextMeshProUGUI text;

    void Start()
    {
        TryGetComponent<TextMeshProUGUI>(out text);
        text.text = "";
        interactor.Found += (interactable) =>
        {
            text.text = interactable.InteractionDescription();
        };
        interactor.Lost += (interactable) =>
        {
            text.text = "";
        };
    }

}
