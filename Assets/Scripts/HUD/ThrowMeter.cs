using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
;

public class ThrowMeter : MonoBehaviour
{

    public SlimePuppeteer slimePuppeteer;
    public RectTransform background;

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<Slider>(out slider);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp = (slimePuppeteer.force - slimePuppeteer.minMaxForce.x) / (slimePuppeteer.minMaxForce.y - slimePuppeteer.minMaxForce.x);
        slider.value = lerp;


        if (lerp <= 0)
        {
            background.gameObject.SetActive(false);
        }
        else
        {
            background.gameObject.SetActive(true);
        }

    }
}
