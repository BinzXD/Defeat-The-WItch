using UnityEngine;
using UnityEngine.SceneManagement; // Untuk menggunakan SceneManager
using System.Collections;

public class NPCShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public Transform bulletPos;
    private float timer;
    public float speed ;
    [SerializeField] float health, maxHealth = 10f;
    [SerializeField] FloatingHealthBar healthBar;
    private bool facingRight = true;
    private Rigidbody2D rb;

    private int shotsTaken = 0; // Counter untuk jumlah tembakan yang diterima
    private int maxShots = 10; // Jumlah maksimum tembakan sebelum NPC mati

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        Debug.Log($"Health after damage: {health}");
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        // Hancurkan NPC dari scene dengan jeda 5 detik
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        SceneManager.LoadScene("berhasil");
    }

    public void OnBulletHit()
    {
        float damageAmount = 1f; // Setiap tembakan mengurangi 1 health point
        TakeDamage(damageAmount);

        shotsTaken++; // Tambahkan counter tembakan yang diterima
        Debug.Log($"Shots taken: {shotsTaken}");

        // Cek apakah jumlah tembakan sudah mencapai batas maksimum
        if (shotsTaken >= maxShots)
        {
            StartCoroutine(Die());
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance < 20)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                Shoot();
            }

            MoveTowardsPlayer();
            Flip();
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        newBullet.GetComponent<EnemyBulletScript>().force = speed;
    }

    private void Flip()
    {
        if ((player.transform.position.x > transform.position.x && facingRight) ||
            (player.transform.position.x < transform.position.x && !facingRight))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
}
