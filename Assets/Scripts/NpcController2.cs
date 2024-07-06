using UnityEngine;

public class NpcController2 : MonoBehaviour
{
    public float speed = 2f;
    public Transform batas1;
    public Transform batas2;
    [SerializeField] private float damage;

    private bool movingRight = true;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 leftBoundary;
    private Vector2 rightBoundary;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Kelelawar tidak terpengaruh gravitasi

        leftBoundary = batas1.position;
        rightBoundary = batas2.position;

        // Mengatur animasi terbang
        animator.SetBool("isFlying", true);

        // Jika kelelawar menghadap kiri saat start, kita balikkan arah awalnya
        if (transform.localScale.x > 0)
        {
            Flip();
        }
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= rightBoundary.x)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= leftBoundary.x)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with " + collision.name); // Debug log
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit!"); // Debug log
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
