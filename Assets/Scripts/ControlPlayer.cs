using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlMode
{
    Mobile,
    PC,
    Console
}

public class ControlPlayer : MonoBehaviour
{
    public ControlMode controlmode; // Properti untuk menentukan mode kontrol
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Pastikan gravityScale sesuai
        rb.gravityScale = 1f; // Anda bisa menyesuaikan nilai ini jika perlu
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        // Move the player
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Update the animator's isWalk parameter
        animator.SetBool("isWalk", move != 0);

        // Handle character flip
        if (move < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (move > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Jumping with "Jump" button or up arrow key
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.Play("WalkKsatria");
        }

        // Update the animator's idle and walking state
        if (isGrounded)
        {
            if (move == 0)
            {
                animator.Play("IdleKsatria");
            }
            else
            {
                animator.Play("WalkKsatria");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
