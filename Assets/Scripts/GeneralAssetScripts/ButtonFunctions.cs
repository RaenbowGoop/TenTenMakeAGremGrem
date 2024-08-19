using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // Play hover SFX once on hover
    public void HoverSound()
    {
        mySounds.volume = 0.5f;
        mySounds.PlayOneShot(hoverSound);
    }

    // Play Click SFX once on Click
    public void ClickSound()
    {
        mySounds.volume = 1f;
        mySounds.PlayOneShot(clickSound);
    }
}