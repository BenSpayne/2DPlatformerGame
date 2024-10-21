using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextlevel : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D other)
   {
    if (other.gameObject.tag == "Player")
    { 
        // display win message
        // Delay for visual effect (optional)
        Invoke("LoadNextLevel", 0f);
    }
   }
   private void LoadNextLevel()
   {
       SceneManager.LoadScene("Manor");
    }
    
}

