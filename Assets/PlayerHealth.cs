using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        health = health;
    }

    void Update()
    {

        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
    
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else 
            {
                hearts[i].sprite = emptyHeart;
            }
            {
                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                } 
                else 
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health = health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    
}
