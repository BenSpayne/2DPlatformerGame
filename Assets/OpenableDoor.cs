using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    [SerializeField]
    GameObject switchOn;

    [SerializeField]
    GameObject switchOff;

    public bool isOn = false; 
    void start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;

        isOn = true;
    }




}
