using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public float BGMVolumeSetting;
    public float SFXVolumeSetting;

    private void OnApplicationQuit()
    {
        // Saving Volume Settings
        PlayerPrefs.SetFloat("BGMVolumeSetting", BGMVolumeSetting);
        PlayerPrefs.SetFloat("SFXVolumeSetting", SFXVolumeSetting);
    }

    private void Start()
    {
        // Loading Volume Settings
        BGMVolumeSetting = PlayerPrefs.GetFloat("BGMVolumeSetting", .35f);
        SFXVolumeSetting = PlayerPrefs.GetFloat("SFXVolumeSetting", .35f);
    }
}
