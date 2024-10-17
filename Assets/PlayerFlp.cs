using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
    
    
public class PlayerFlip : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    private float horizontalInput;
    private bool facingRight = true;
    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        SetupDirectionByComponent();
    }
    private void SetupDirectionByScale()
    {
        if (horizontalInput < 0 && facingRight || horizontalInput > 0 && !facingRight)
        {
            facingRight = !facingRight;
            UnityEngine.Vector3 playerScale = transform.localScale;
            playerScale.x *= -1; 
            transform.localScale = playerScale;
        }
    }

    private void SetupDirectionByComponent()
    {
        if (horizontalInput < 0)
            _spriteRenderer.flipX = true;
        else if (horizontalInput > 0)
            _spriteRenderer.flipX = false;
    }
}  
