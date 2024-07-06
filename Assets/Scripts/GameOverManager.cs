using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void ReplayGame()
    {
        // Muat ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToHome()
    {
        // Muat ulang scene dengan index 0 (diasumsikan sebagai menu utama)
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Keluar dari aplikasi
        Application.Quit();
    }
}
