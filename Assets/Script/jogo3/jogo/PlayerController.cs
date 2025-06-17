using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    public GameObject oilSplashPrefab;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Transform currentPlatform;

    private GameOverManager gameOverManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverManager = GameObject.FindObjectOfType<GameOverManager>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Debug.Log("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Encostou na plataforma");
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform)
        {
            transform.parent = null;
            currentPlatform = null;
            
        }
    }

    
}