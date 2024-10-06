using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Panel;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void AboutPage()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void MainMenuReturn()
    {
        SceneManager.LoadSceneAsync(0);

    }
    public void AboutScreen_2()
    {
        SceneManager.LoadSceneAsync(3);

    }
    public void PauseScreen()
    {
        Panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void unpauseScreen()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }
  

}
