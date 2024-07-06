using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void update()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void Pilih()
    {
        SceneManager.LoadScene("Select");
    }
    public void Credit()
    {
        SceneManager.LoadScene("Credit");        
    }


    public void QuitGame(){
        Application.Quit();
    }

    public void OpenAreYouSure(){
        quitPanel.SetActive(true);
    }

    public void CloseAreYouSure(){
        quitPanel.SetActive(false);
    }
}
