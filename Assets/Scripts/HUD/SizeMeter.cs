using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SizeMeter : MonoBehaviour
{

    public SlimePuppeteer slimePuppeteer;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = slimePuppeteer.CurrentSlime.Size.ToString();
        slimePuppeteer.SlimeChanged += (slime) =>
        {
            text.text = slime.Size.ToString();
        };
    }

}
