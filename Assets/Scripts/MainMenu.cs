using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject easyList;
    [SerializeField] private GameObject mediumList;
    [SerializeField] private GameObject hardList;
    [SerializeField] private GameObject hardcoreList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void easy()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Easy");
    }
    public void medium()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Medium");
    }
    public void hard()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Hard");
    }

    public void hardcore()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Hardcore");
    }
    public void easyMonsters()
    {
        easyList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void mediumMonsters()
    {
        mediumList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void hardMonsters()
    {
        hardList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void hardcoreWarning()
    {
        hardcoreList.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void backToLevelMenu()
    {
        levelMenu.SetActive(true);
        easyList.SetActive(false);
        mediumList.SetActive(false);
        hardList.SetActive(false);
        hardcoreList.SetActive(false);
    }

    public void playGame()
    {
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);

    }

    public void backToMainMenu()
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
