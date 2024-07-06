using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private GameObject PanelSelesai; // Pastikan nama variabel konsisten


    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
        PanelSelesai.SetActive(false); // Pastikan panel game over tidak aktif di awal
    }
    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
        if (currenthealthBar.fillAmount == 0)
        {
            PanelSelesai.SetActive(true); // Aktifkan panel game over ketika fillAmount 0
        }
    }
}