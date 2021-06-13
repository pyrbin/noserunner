using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MovableObject : MonoBehaviour

{
    public Vector3 target;
    public float speed;


    Vector3 startPos;
    Vector3 endPos;

    private bool isMoving = false;


    [FMODUnity.EventRef]
    public string sound;
    FMOD.Studio.EventInstance soundEvent;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + target;
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(sound);
    }


    void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        if (isMoving && Vector3.Distance(transform.position, endPos) >= 0.001f)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            return;
        }
        else if (!isMoving && Vector3.Distance(transform.position, startPos) >= 0.001f)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            return;
        }

        soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    public void Move(Interactor user)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        soundEvent.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            soundEvent.start();
        }
        isMoving = true;
    }


    public void StopMove()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        soundEvent.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            soundEvent.start();
        }
        isMoving = false;
    }


}
