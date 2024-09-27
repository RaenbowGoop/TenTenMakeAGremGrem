using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonBehavior : MonoBehaviour
{
    [SerializeField] GameObject obj;

    public void disableGameObject()
    {
        obj.SetActive(false);
    }
}
