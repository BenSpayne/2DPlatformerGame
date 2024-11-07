using System.Numerics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour 
{

    private UnityEngine.Vector3 respawnpoint;
    private bool isJumping;
    public MenuManager menuManager;
    public GameController gameController;
    public int health;
    private Rigidbody2D rb;
    public AudioClip coinSound;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public LayerMask enemyLayers;
    private Animator animator;
    private BoxCollider2D boxColl;    
    private float horizontal;
    private bool isFacingRight;

    [Header("Basic Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 6f;

    private float originalSpeed;

    [Header("Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Transform spawnPoint;

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();



    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("IsJumping", Mathf.Abs(rb.velocity.y));

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpingPower);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        rb.velocity = new UnityEngine.Vector2(horizontal * speed, rb.velocity.y);

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("IsAttacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); 
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Player hit NPC Enemy");
            //enemy.GetComponent<enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
        }

    void OnDrawGizmosSelected()
    {

        if(attackPoint = null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            gameController.ChangeCoin(1);
            Destroy(other.gameObject);
            Debug.Log("Player has collected a coin!");
        }

        if (other.gameObject.tag == "Enemy")
        {
            health -= 1;
             Debug.Log("Player has hit Enemy");
            transform.position = respawnpoint;
        }
    

        if (other.gameObject.tag == "Checkpoint")
        {
            respawnpoint = transform.position;
        }

        if (other.gameObject.tag == "mapEdge")
        {
             Debug.Log("Player has hit the map Edge");
           
           transform.position = respawnpoint;
            
        }

    }

    


    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            UnityEngine.Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }    
}






/*public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
    public float jumpForce = 10f;       // Jump force for vertical movement
    public Rigidbody2D rb;              // Rigidbody2D component

    private Vector2 movement;           // Store player movement input
    private bool isGrounded;            // Check if the player is grounded
    private bool canDoubleJump;         // Check if the player can double jump
    public Transform groundCheck;       // Ground check position
    public float groundCheckRadius = 0.2f; // Radius for checking ground
    public LayerMask groundLayer;       // Layer used to detect ground

    void Update()
    {
        // Get the horizontal input
        movement.x = Input.GetAxisRaw("Horizontal");

        // Flip the character sprite based on movement direction
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }

        // Jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true; // Allow double jump after the first jump
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false; // Disable double jump after second jump
            }
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Jump()
    {
        // Apply vertical force to jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}

public class PlayerWalk : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
    public float jumpForce = 15f;       // Jump force for vertical movement
    public Animator animator;           // Animator for controlling animations
    
    private Rigidbody2D rb;             // Rigidbody2D component
    private bool isFacingRight = true;  // Track whether the player is facing right
    private bool isGrounded;            // Check if the player is grounded
    private bool canDoubleJump;         // Check if the player can double jump
    private bool isJumping;             // Check if the player is currently jumping
    
    private Transform groundCheck;      // Ground check position
    public float groundCheckRadius = 0.2f; // Radius for checking ground
    private LayerMask groundLayer;      // Layer used to detect ground
    
    void Start()
    {
        // Automatically get references to Rigidbody2D, groundCheck, and groundLayer
        rb = GetComponent<Rigidbody2D>();                  // Get the Rigidbody2D component of the player.
        groundCheck = transform.Find("GroundCheck");       // Find the ground check position (assumes a child object named "GroundCheck").
        groundLayer = LayerMask.GetMask("Ground");         // Set the ground layer (replace "Ground" with the actual layer name).

        rb.freezeRotation = true; // Lock the character's rotation.
    }

    void Update()
    {
        // Get the horizontal input (A/D or Left/Right arrow keys)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Handle character flip when moving left or right
        if ((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight))
        {
            FlipCharacter();
        }

        // Check if the player is grounded and handle jumping logic
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input using the Space Bar (or "Jump" button in Input Manager)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true; // Allow double jump after the first jump
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false; // Disable double jump after second jump
            }
        }

        // Control the animation based on whether the player is jumping or not
        if (!isGrounded)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        // Update the Animator
        animator.SetBool("isJumping", isJumping); // Set jumping state
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x)); // Set speed based on horizontal velocity
    }

    void FixedUpdate()
    {
        // Get the horizontal input and apply horizontal movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        // Apply vertical force to jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Debug to check if the jump function is being called and the velocity is being set
        Debug.Log("Jump executed. Velocity: " + rb.velocity);
    }

    private void FlipCharacter()
    {
        // Toggle the facing direction flag
        isFacingRight = !isFacingRight;

        // Invert the X scale to flip the character horizontally
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}*/

/*public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public Animator animator;

    private float move;
    private bool isJumping;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Manager GameManager;

    private UnityEngine.Vector3 respawnpoint;
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
        animator.SetFloat("IsFalling", Mathf.Abs(rb.velocity.x));


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
             Debug.Log("Player has hit Enemy");
           
           transform.position = respawnpoint;
            
        }

        if (other.gameObject.tag == "Checkpoint")
        {
            respawnpoint = transform.position;
        }
    }
}*/



