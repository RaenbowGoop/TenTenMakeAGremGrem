using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceEntrance;
    [SerializeField] private AudioSource audioSourceLoop;
    [SerializeField] private AudioClip musicEntrance; 

    IEnumerator WaitForEntranceFinish()
    {
        float musicEntranceLength = musicEntrance.length;
        yield return new WaitForSeconds(musicEntranceLength);

        // Switch to Loop track
        audioSourceEntrance.Stop();
        audioSourceLoop.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Switch in Loop track if Entrance is done
        StartCoroutine(WaitForEntranceFinish());
    }
}
