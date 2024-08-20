using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    // Start is called before the first frame update
    void Awake()
    {

        HideSettingsMenu();
    }

    // Displays Settings Menu
    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    // Hides Settings Menu
    public void HideSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
}
