using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotepadButtonManager : MonoBehaviour
{
    [SerializeField] GameObject notePadBackground;
    [SerializeField] GameObject resultInfo;

    [SerializeField] Sprite resultBG;
    [SerializeField] Sprite basePointInfoBG;
    [SerializeField] Sprite multiplierInfoBG;


    public void changeToResultPage()
    {
        notePadBackground.GetComponent<Image>().sprite = resultBG;
        resultInfo.SetActive(true);
        Debug.Log("res");
    }


    public void changeToBasePointInfo()
    {
        notePadBackground.GetComponent<Image>().sprite = basePointInfoBG;
        resultInfo.SetActive(false);
        Debug.Log("base");
    }
    public void changeToMultiplierInfo()
    {
        notePadBackground.GetComponent<Image>().sprite = multiplierInfoBG;
        resultInfo.SetActive(false);
        Debug.Log("mul");
    }
}
