using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Include the UI namespace

public class PlayerLife : MonoBehaviour
{
   private Rigidbody2D rb;
   private Animator anim;

   [SerializeField] private AudioSource deathSoundEffect;
   [SerializeField] private int lives = 3; // Set the initial number of lives
   [SerializeField] private Text livesText; // Reference to the UI Text component
   [SerializeField] private Transform spawnPoint; // The spawn point for respawning


   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       UpdateLivesText(); // Initialize the lives display
       Respawn(); // Respawn player at the start
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Trap"))
       {
           LoseLife();
       }

       if (collision.gameObject.CompareTag("Enemy"))
       {
           Die();
       }
   }

   private void Respawn()
   {
       // Optionally: Reset player position and state
       transform.position = spawnPoint.position; // Move player to the spawn point
       rb.bodyType = RigidbodyType2D.Dynamic; // Reset Rigidbody to dynamic
       //anim.SetTrigger("respawn"); // Optionally, trigger respawn animation
       // You might want to reset health or other player states here
   }

   private void LoseLife()
   {
       lives--;
       UpdateLivesText(); // Update the lives display
       if (lives <= 0)
       {
           Die();
       }
       else
       {
           // Optionally: Add feedback for losing a life (like flashing the player)
           Debug.Log("Life lost! Remaining lives: " + lives);
           Respawn(); // Respawn the player


       }
   }

   private void UpdateLivesText()
   {
       livesText.text = "Lives: " + lives; // Update the text to show remaining lives
   }

   private void Die()
   {
       deathSoundEffect.Play();
       rb.bodyType = RigidbodyType2D.Static;
       //anim.SetTrigger("death");
       // Optionally: Restart level after a delay
       Invoke("GoToEndScene", 2f); // Go to end scene after a delay
      // Invoke("RestartLevel", 2f); // Example: Restart after 2 seconds
   }

   private void RestartLevel()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      
   }

    private void GoToEndScene()
   {
       SceneManager.LoadScene("EndScene"); // Replace "EndScene" with the actual name of your end scene
   }
}



