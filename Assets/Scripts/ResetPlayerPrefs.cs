using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Public method to reset all PlayerPrefs data
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs data has been reset.");
    }
}

// Attach this script to a GameObject, e.g., GameController
// Create a UI Button and link the Button's OnClick event to the ResetAllData method
