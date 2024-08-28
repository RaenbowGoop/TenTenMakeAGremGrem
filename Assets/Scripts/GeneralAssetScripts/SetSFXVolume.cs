using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        // Find Player Object
        PlayerDataManager player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerDataManager>();

        // Get Player Volume Settings
        audioSource.volume = player.SFXVolumeSetting;
    }
}
