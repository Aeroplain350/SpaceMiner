using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// mixer mechanics
public class MixerManager : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer audioMixer;

    [Header("Game Objects")]
    public GameObject settingsGameObject;

    public Slider masterSlider, musicSlider, sfxSlider;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterPref = "MasterPref";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";

    private int firstPlayInt;

    [HideInInspector]
    public float masterFloat, musicFloat, sfxFloat;

    public void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            masterFloat = 1f;
            musicFloat = 1f;
            sfxFloat = 1f;
            masterSlider.value = masterFloat;
            musicSlider.value = musicFloat;
            sfxSlider.value = sfxFloat;

            PlayerPrefs.SetFloat(MasterPref, masterFloat);
            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetFloat(SFXPref, sfxFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            masterFloat = PlayerPrefs.GetFloat(MasterPref);
            masterSlider.value = masterFloat;
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;
            sfxFloat = PlayerPrefs.GetFloat(SFXPref);
            sfxSlider.value = sfxFloat;
        }

        UpdateSound();
    }

    public void Update()
    {
        if (settingsGameObject.activeInHierarchy)
        {
            masterFloat = masterSlider.value;
            musicFloat = musicSlider.value;
            sfxFloat = sfxSlider.value;
        }
    }

    public void SaveSoundSetting()
    {
        PlayerPrefs.SetFloat(MasterPref, masterSlider.value);
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(SFXPref, sfxSlider.value);
    }

    //public void OnApplicationFocus(bool inFocus)
    //{
    //    if (!inFocus)
    //    {
    //        SaveSoundSetting();
    //    }
    //}

    public void UpdateSound()
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(masterFloat <= 0 ? 0.001f : masterFloat) * 30f);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(musicFloat <= 0 ? 0.001f : musicFloat) * 30f);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(sfxFloat <= 0 ? 0.001f : sfxFloat) * 30f);
    }
}