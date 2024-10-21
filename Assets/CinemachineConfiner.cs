using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineConfiner : MonoBehaviour
{

    [SerializeField] private GameObject Vcam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Vcam.SetActive(true);
        }
    }
    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Vcam.SetActive(false);
        }
    }

}
