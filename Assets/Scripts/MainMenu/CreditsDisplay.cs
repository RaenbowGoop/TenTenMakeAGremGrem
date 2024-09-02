using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject creditsObj;
    public void openCreditsTab() {
        creditsObj.SetActive(true);
    }

    public void closeCreditsTab()
    {
        creditsObj.SetActive(false);
    }
}
