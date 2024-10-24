using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChestController : MonoBehaviour
{
    public bool isOpen; 
    public Light2D openLight;
    public Animator animator;

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log ("Chest is now open..,");
            animator.SetBool("isOpen", isOpen);
            openLight.enabled = true;
        }
    }
}
