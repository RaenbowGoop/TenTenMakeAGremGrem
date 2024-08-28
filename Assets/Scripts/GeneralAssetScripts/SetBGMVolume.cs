using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBGMVolume : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;


    // Start is called before the first frame update
    void Start()
    {
        // Find Player Object
        PlayerDataManager player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerDataManager>();

        // Get Player Volume Settings
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = player.BGMVolumeSetting;
        }
    }
}
