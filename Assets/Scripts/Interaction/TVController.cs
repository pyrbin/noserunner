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
        [FMODUnity.EventRef]
        public string sound;
    }

    [SerializeField]
    public List<TVChannel> channels;
    public SpriteRenderer display;
    public FMOD.Studio.EventInstance soundState;

    [FMODUnity.EventRef]
    public string changeSound;

    private int currIndex = 0;

    void Start()
    {
        display.sprite = channels[currIndex].Image;
        soundState = FMODUnity.RuntimeManager.CreateInstance(channels[currIndex].sound);
        soundState.start();
    }

    private void Switch()
    {

        FMODUnity.RuntimeManager.PlayOneShot(changeSound, display.gameObject.transform.position);

        soundState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        currIndex = (currIndex + 1) % channels.Count;
        display.sprite = channels[currIndex].Image;
        soundState = FMODUnity.RuntimeManager.CreateInstance(channels[currIndex].sound);
        soundState.start();
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
