using System.Collections;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float speed = 2f;
    public Transform batas1;
    public Transform batas2;
    public float idleDuration = 2f;

    private bool isWalking = false;
    private bool movingRight = true;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 leftBoundary;
    private Vector2 rightBoundary;

    [SerializeField] private float damage;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        leftBoundary = batas1.position;
        rightBoundary = batas2.position;
        StartCoroutine(Idle());
    }

    void Update()
    {
        if (isWalking)
        {
            Move();
        }
    }

    private IEnumerator Idle()
    {
        isWalking = false;
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(idleDuration);
        isWalking = true;
        animator.SetBool("isWalking", true);
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
                StartCoroutine(Idle());
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= leftBoundary.x)
            {
                movingRight = true;
                Flip();
                StartCoroutine(Idle());
            }
        }
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name); // Debug log
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!"); // Debug log
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
