using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseClickAnimationSpawner : MonoBehaviour
{
    
    [SerializeField] Animator mouseAnim;
    [SerializeField] TextMeshProUGUI animationText;
    [SerializeField] GameObject mouseAnimObj;
    System.Random rng;

    void Start()
    {
        rng = new System.Random();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Generate Random Color (lowerbound: 0.3f)
            float red = (float)rng.NextDouble()*0.7f;
            float green = (float)rng.NextDouble()* 0.7f;
            float blue = (float)rng.NextDouble()* 0.7f;
            animationText.color = new Color(red, green, blue, 1.0f);

            // Set animation object to mouse position
            mouseAnimObj.transform.position = Input.mousePosition;

            // trigger animation condition
            mouseAnim.SetBool("Active", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            // turn off animation trigger
            mouseAnim.SetBool("Active", false);
        }
    }
}
