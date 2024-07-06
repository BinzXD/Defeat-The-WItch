using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text coinText; // Komponen TextMeshPro untuk menampilkan jumlah koin

    [SerializeField] private ControlPlayer controlPlayer; // Komponen ControlPlayer yang akan digunakan

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;
    private Vector3 playerPosition;

    // Level Complete
    [SerializeField] private GameObject levelCompletePanel; // Panel untuk level complete
    [SerializeField] private TMP_Text levelCompletePanelTitle; // Teks judul di panel level complete
    [SerializeField] private TMP_Text levelCompleteCoins; // Teks jumlah koin di panel level complete

    private int totalCoins = 0;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        playerPosition = controlPlayer.transform.position;
        FindTotalPickups();
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
    }

    public void Death()
    {
        if (!isGameOver)
        {
            UIManager.instance.DisableMobileControls();
            UIManager.instance.fadeToBlack = true;
            controlPlayer.gameObject.SetActive(false);
            StartCoroutine(DeathCoroutine());
            isGameOver = true;
            Debug.Log("Died");
        }
    }

    public void FindTotalPickups()
    {
        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();
        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }
        }
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
        levelCompletePanelTitle.text = "";
        levelCompleteCoins.text = "" + coinCount.ToString() + " / " + totalCoins.ToString();
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        controlPlayer.transform.position = playerPosition;

        yield return new WaitForSeconds(1f);

        if (isGameOver)
        {
            SceneManager.LoadScene(1);
        }
    }
    public int GetCoinCount()
    {
        return coinCount;
    }
}
