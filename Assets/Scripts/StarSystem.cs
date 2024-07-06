using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Image[] stars; // Array untuk menyimpan referensi gambar bintang
    [SerializeField] private int totalPoints = 10; // Total poin yang dapat dikumpulkan
    [SerializeField] private float maxHealth = 3f; // Nilai maksimal kesehatan

    private void Start()
    {
        // Pastikan semua bintang tidak aktif di awal
        foreach (Image star in stars)
        {
            star.gameObject.SetActive(false);
        }

        int starCount = CalculateStars();
        DisplayStars(starCount);
    }

    private int CalculateStars()
{
    // Menghitung persentase kesehatan dan poin
    float healthPercentage = playerHealth.currentHealth / maxHealth;
    float pointPercentage = (float)gameManager.GetCoinCount() / totalPoints;

    // Log untuk debugging
    Debug.Log($"Health Percentage: {healthPercentage * 100}%");
    Debug.Log($"Point Percentage: {pointPercentage * 100}%");

    // Menentukan jumlah bintang berdasarkan persentase
    if (healthPercentage >= 0.75f && pointPercentage >= 0.75f)
    {
        return 3; // 3 bintang
    }
    else if (healthPercentage >= 0.5f && pointPercentage >= 0.5f)
    {
        return 2; // 2 bintang
    }
    else
    {
        return 1; // 1 bintang
    }
}

    private void DisplayStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
    }
}