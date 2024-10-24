using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 80;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {

            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
