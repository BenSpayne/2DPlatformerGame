using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toFinalWorld : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D other)
   {
    if (other.gameObject.tag == "Player")
    { 
        // display win message
        // Delay for visual effect (optional)
        Invoke("LoadFinalWorld", 0f);
    }
   }
   private void LoadFinalWorld()
   {
       SceneManager.LoadScene("FinalWorld");
    }
    
}
