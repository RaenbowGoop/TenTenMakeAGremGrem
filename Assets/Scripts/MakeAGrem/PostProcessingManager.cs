using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] UnityEngine.Rendering.PostProcessing.PostProcessVolume postProcessVolume;
    [SerializeField] UnityEngine.Rendering.PostProcessing.PostProcessLayer postProcessLayer;
    [SerializeField] GameObject iOSDarkenScreen;

    [SerializeField] bool buildingForiOSMobile;

    void Awake()
    {
        // Disable iOS Screen by default
        iOSDarkenScreen.SetActive(false);

        // Disable post processing and enable iOS darken screen if on iOS
        if (buildingForiOSMobile )
        {
            postProcessVolume.enabled = false;
            postProcessLayer.enabled = false;
            iOSDarkenScreen.SetActive(true);
        }
    }
}
