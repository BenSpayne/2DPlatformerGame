using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class InteractLever : MonoBehaviour
{
    public bool leverInRange;
    public KeyCode interactKey;
    public UnityEvent interactionAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leverInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactionAction.Invoke();
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            leverInRange = true; 
            Debug.Log("Lever in range"); 

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("Player"))
        {
            leverInRange = false; 
            Debug.Log("Player now not in range of lever"); 

        }

    }
}

