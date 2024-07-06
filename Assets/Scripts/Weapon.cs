using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // Asumsi tembakan selalu ke kanan

        Destroy(bullet, 1f); // Hancurkan peluru setelah beberapa detik

        // Mencari semua objek NPC di scene
        NPCShoot[] npcs = FindObjectsOfType<NPCShoot>();
        foreach (NPCShoot npc in npcs)
        {
            npc.OnBulletHit(); // Panggil method OnBulletHit di setiap NPC
        }
    }
}
