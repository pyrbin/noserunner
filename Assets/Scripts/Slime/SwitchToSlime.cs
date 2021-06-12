using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Slime))]
public class SwitchToSlime : Interactable
{
    public SlimePuppeteer Puppeteer;

    private Slime slime;

    protected void Awake()
    {
        slime ??= GetComponent<Slime>();
    }

    protected override void OnInteract(Interactor user)
    {
        Puppeteer.SwitchSlime(slime);
    }
}
