using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickAnimationSpawner : MonoBehaviour
{
    
    [SerializeField] Animator mouseAnim;
    [SerializeField] GameObject mouseAnimObj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseAnimObj.transform.position = Input.mousePosition;
            mouseAnim.SetBool("Active", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseAnim.SetBool("Active", false);
        }
    }
}
