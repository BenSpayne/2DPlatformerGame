using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LeverController : MonoBehaviour
{
    public bool isOn; 
    public Light2D openLight;
    public Animator animator;

    public void OpenChest()
    {
        if (!isOn)
        {
            isOn = true;
            Debug.Log ("Lever is now on..,");
            animator.SetBool("Lever Animation", isOn);
            openLight.enabled = true;
        }
    }
}
