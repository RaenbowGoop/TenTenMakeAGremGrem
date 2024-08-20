using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject musicSlider;
    [SerializeField] GameObject sfxSlider;
    float musicVolume;
    float sfxVolume;

    // set music volume
    public void SetMusicVolume()
    {
        musicVolume = musicSlider.GetComponent<Slider>().value;
    }

    // set sfx volume
    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.GetComponent<Slider>().value;
    }

    // sets volume of all music in current scene
    public void SetAllMusicVolume()
    {
        GameObject[] musicList = GameObject.FindGameObjectsWithTag("BGM");
        foreach(GameObject music in musicList)
        {
            music.GetComponent<AudioSource>().volume = musicVolume;
        }
    }

    // sets volume of sfx in current scene
    public void SetAllSFXVolume()
    {
        GameObject[] musicList = GameObject.FindGameObjectsWithTag("SFX");
        foreach (GameObject music in musicList)
        {
            music.GetComponent<AudioSource>().volume = sfxVolume;
        }
    }

    // on scene load:
    //   if reloading into MainMenu, relink volume sliders
    //   if loading into a different scene, set volume for music and sfx in current scene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            musicSlider = GameObject.Find("MusicVolumeSlider");
            sfxSlider = GameObject.Find("SFXVolumeSlider");
        }
        SetMusicVolume();
    }
}
