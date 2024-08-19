using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    [SerializeField] string gameTag;

    private void Awake()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag(gameTag);
        if (gameObject.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
