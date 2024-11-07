using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
   private int coinCounter = 0;

   [SerializeField] private TextMeshProUGUI coinText;
   [SerializeField] private AudioClip coinSound;
   [SerializeField] private PlayerLife playerLife; // Reference to PlayerLife script

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("Coin"))
       {
           AudioSource.PlayClipAtPoint(coinSound, transform.position);
           Destroy(collision.gameObject);
           coinCounter++;
           coinText.text = "Coins: " + coinCounter;         
       }
   }
}


