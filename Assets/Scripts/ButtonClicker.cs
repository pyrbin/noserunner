using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicker : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string sound;
    public void clicker(){
        FMODUnity.RuntimeManager.PlayOneShot(sound);
    }
}
