using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] PlayableDirector darkenAnimationNormal;
    [SerializeField] PlayableDirector lightenAnimationNormal;
    [SerializeField] PlayableDirector darkenAnimationMobile;
    [SerializeField] PlayableDirector lightenAnimationMobile;

    [SerializeField] ColorTimeManager colorTimeManager;
    [SerializeField] PostProcessVolume postProcessVolume;
    [SerializeField] PostProcessLayer postProcessLayer;
    [SerializeField] GameObject iOSDarkenScreen;

    [SerializeField] bool buildingForiOSMobile;

    void Awake()
    {
        // Disable iOS Screen by default
        iOSDarkenScreen.SetActive(false);

        // Disable post processing and enable iOS darken screen if on iOS
        if (buildingForiOSMobile )
        {
            // Disable Post processing effects and enable iOS Darken Screen
            postProcessVolume.enabled = false;
            postProcessLayer.enabled = false;
            iOSDarkenScreen.SetActive(true);

            // Set Lighting Animations to the Mobile friendly version
            colorTimeManager.darkenAnimation = darkenAnimationMobile;
            colorTimeManager.lightenAnimation = lightenAnimationMobile;
        } else {
            // Set Lighting Animations to the normal timeline animations (with post processing)
            colorTimeManager.darkenAnimation = darkenAnimationNormal;
            colorTimeManager.lightenAnimation = lightenAnimationNormal;
        }

        // Set Flag for ColorTimeManager
        colorTimeManager.buildForiOSMobile = buildingForiOSMobile;
        
    }
}
