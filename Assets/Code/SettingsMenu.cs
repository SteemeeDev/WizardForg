using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    public void SetAudio()
    {
        audioMixer.SetFloat("Volume", volumeSlider.value);
    }
}
