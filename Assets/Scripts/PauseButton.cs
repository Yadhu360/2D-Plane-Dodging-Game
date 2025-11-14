using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    static public PauseButton instance;
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUi;

    public Button pauseButton, resumeButton, menuButton,quitButton;

    void Start()
    {
        GameIsPaused = false;
    }

    private void Awake()
    {
        instance = this;
      
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(ResumeFunction);
        menuButton.onClick.AddListener(LoadMenu);
        quitButton.onClick.AddListener(Quit);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeFunction();
            }
            else
            {
                Pause();
            }
        }
    }
    //Resume GamePlay function
    public void ResumeFunction()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    //The pause function
    public void Pause()
    {
           
            PauseMenuUi.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        
    }
    // Go to the main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }
    // Quitting game
    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
