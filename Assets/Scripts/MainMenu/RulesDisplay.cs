using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    [SerializeField] GameObject rulesObj;
    public void openRulesTab() {
        rulesObj.SetActive(true);
    }

    public void closeRulesTab()
    {
        rulesObj.SetActive(false);
    }
}
