using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicSlider;
    public void SetAudio()
    {
        audioMixer.SetFloat("Volume", volumeSlider.value);
        if (musicSlider.value <= musicSlider.minValue + 0.01f) audioMixer.SetFloat("MusicVolume", -80f);
        else audioMixer.SetFloat("MusicVolume", musicSlider.value);
    }
}
