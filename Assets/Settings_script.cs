using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Settings_script : MonoBehaviour
{

        public GameObject mainMenuHolder;
        public GameObject optionsMenuHolder;

        public Slider[] volumeSliders;
        public Toggle[] resolutionToggles;
        public int[] screenWidths;
        int activeScreenResIndex;

    public void SetScreenResolution(int i) {
        if(resolutionToggles[i].isOn) {
            activeScreenResIndex = i;
            float aspectRatio = 16/9f;  
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i]/aspectRatio), false);
        }

    }

    public void SetFullscreen(bool isFullscreen) {
        for(int i=0;i<resolutionToggles.Length;i++) {
            resolutionToggles[i].interactable = !isFullscreen;
        }
        if(isFullscreen) {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolutions = allResolutions[allResolutions.Length-1];
            Screen.SetResolution(maxResolutions.width, maxResolutions.height, true);
        } else {
            SetScreenResolution(activeScreenResIndex);
        }
    }

    public void SetMasterVolume(float value) {

    }

    public void SetMusicVolume(float value) {

    }
    public void SetSfxVolume(float value) {

    }
}
