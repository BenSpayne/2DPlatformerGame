using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    private float move;
    private bool isJumping;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Manager GameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        GameManager = GameObject.Find("GameManager").GetComponent<Manager>();

    }

    private void Update()
    {
        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && isJumping == false)
        {
            isJumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jump));
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
        }

    

}
