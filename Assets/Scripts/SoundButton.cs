using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite activeSound, disableSound;
    public void DoOnClick()
    {
        AudioListener mainCameraAudioListener = Camera.main.GetComponent<AudioListener>();

        if (mainCameraAudioListener.enabled)
        {
            mainCameraAudioListener.enabled = false;
            GetComponent<Image>().sprite = disableSound;
        } else
        {
            mainCameraAudioListener.enabled = true;
            GetComponent<Image>().sprite = activeSound;
        }
    }
}
