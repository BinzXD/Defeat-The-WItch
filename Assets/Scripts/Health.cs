using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Tambahkan namespace ini

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float invulnerabilityDuration = 2f; // Durasi invulnerability
    [SerializeField] private float blinkInterval = 0.1f; // Interval berkedip

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("gameover");
    }

    public void TakeDamage(float _damage)
    {
        if (isInvulnerable) return; // Jika invulnerable, tidak menerima damage

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(InvulnerabilityCoroutine()); // Memulai invulnerability
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerController>().enabled = false;
                dead = true;

                StartCoroutine(LoadNextScene()); // Memulai coroutine untuk load scene berikutnya
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        float endTime = Time.time + invulnerabilityDuration;

        while (Time.time < endTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Berkedip
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true; // Pastikan sprite aktif saat invulnerability berakhir
        isInvulnerable = false;
    }
}
