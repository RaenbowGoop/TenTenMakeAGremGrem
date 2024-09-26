using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionString : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Version " + Application.version + " | Platform: " + Application.platform;
    }
}
