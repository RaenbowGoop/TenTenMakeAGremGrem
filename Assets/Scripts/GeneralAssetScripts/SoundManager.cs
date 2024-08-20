using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject musicSlider;
    [SerializeField] GameObject sfxSlider;
    public PlayerDataManager player;

    float musicVolume;
    float sfxVolume;

    // On Start, Set Players volume settings
    void Start()
    {
        // Find Player Object
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerDataManager> ();

        // Get Player Volume Settings
        musicVolume = player.BGMVolumeSetting;
        sfxVolume = player.SFXVolumeSetting;

        // Set Slider Values
        musicSlider.GetComponent<Slider>().value = musicVolume;
        sfxSlider.GetComponent<Slider>().value = sfxVolume;

        // Set BGM and SFX Volume Levels
        SetAllMusicVolume();
        SetAllSFXVolume();
    }

    // set music volume
    public void SetMusicVolume()
    {
        musicVolume = musicSlider.GetComponent<Slider>().value;

        // Saving Music Volume Settings
        player.BGMVolumeSetting = musicVolume;
    }

    // set sfx volume
    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.GetComponent<Slider>().value;

        // Saving SFX Volume Settings
        player.SFXVolumeSetting = sfxVolume;
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
}
