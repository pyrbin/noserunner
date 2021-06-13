using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : Interactable
{

    [System.Serializable]
    public struct TVChannel
    {
        [SerializeField]
        public Sprite Image;
        [SerializeField]
        public FMOD.Sound Sound;
    }

    [SerializeField]
    public List<TVChannel> channels;

    public SpriteRenderer display;

    private int currIndex = 0;

    void Start()
    {
        display.sprite = channels[currIndex].Image;
    }

    private void Switch()
    {
        currIndex = (currIndex + 1) % channels.Count;
        display.sprite = channels[currIndex].Image;
    }

    protected override void OnInteract(Interactor user)
    {
        Switch();
    }

    public override string InteractionDescription()
    {
        return "Switch channel";
    }
}
