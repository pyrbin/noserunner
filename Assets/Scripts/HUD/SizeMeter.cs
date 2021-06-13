using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SizeMeter : MonoBehaviour
{
    public SlimePuppeteer slimePuppeteer;
    public TextMeshProUGUI text;

    float ScaleClamped => math.clamp(slimePuppeteer.CurrentSlime.Size * 0.5f, .8f, 5f);

    void Update()
    {
        if (slimePuppeteer)
        {
            text.text = slimePuppeteer.CurrentSlime.Size.ToString();
            transform.parent.localScale = new float3(ScaleClamped, ScaleClamped, ScaleClamped);
        }
    }
}
