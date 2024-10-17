using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public Animator animator;

    private float move;
    private bool isJumping;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Manager GameManager;

    public MenuManager menuManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        GameManager = GameObject.Find("GameManager").GetComponent<Manager>();

    }

    private void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.velocity = new (speed * move, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("IsJumping", Mathf.Abs(rb.velocity.y));
        //animator.SetFloat("IsFalling", Mathf.Abs(rb.velocity.x));


        if(Input.GetButtonDown("Jump") && isJumping == false)
        {
            isJumping = true;
            rb.AddForce(new (rb.velocity.x, jump));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            GameManager.coinsCounter += 1;
            Destroy(other.gameObject);
            Debug.Log("Player has collected a coin!");
        }

        if (other.gameObject.tag == "Enemy")
        {
             Debug.Log("Player has hit spike");
           
            menuManager.ChangeScene("GameOverScreen");
        }
    }
}