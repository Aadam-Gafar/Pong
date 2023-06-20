using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    // Creating a global instance of Interface.
    public static Interface instance;


    // Title scene variables.
    public GameObject mainMenu;
    public GameObject difficultyMenu;
    public GameObject creditsMenu;

    public AudioSource clickSFX;
    public AudioSource startSFX;

    // Game scene variables.
    public int targetFPS;

    private int scorePlayer;
    private int scoreAI;
    public Text scoreText;

    public GameObject pauseMenu;
    public GameObject controlsMenu;

    private void Awake()
    {
        CheckInstance();
    }

    void Start()
    {
        clickSFX = Audio.instance.clickSFX;
        startSFX = Audio.instance.startSFX;

        if(SceneManager.GetActiveScene().name == "Game")
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFPS;

            scorePlayer = 0;
            scoreAI = 0;

            Time.timeScale = 0;
            pauseMenu.SetActive(false);
            controlsMenu.SetActive(false);
            StartCoroutine(Countdown());
        }
        else if (SceneManager.GetActiveScene().name == "Title")
        {
            mainMenu.SetActive(true);
            difficultyMenu.SetActive(false);
            creditsMenu.SetActive(false);
        }
    }

    #region Title scene methods
    public void ChooseDifficulty()
    {
        mainMenu.SetActive(false);
        difficultyMenu.SetActive(true);
    }

    public void ChooseEasy()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        LoadGame();
    }

    public void ChooseMedium()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        LoadGame();
    }

    public void ChooseHard()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        LoadGame();
    }

    public void ViewCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        difficultyMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Game scene methods
    public void Pause()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);

        int time = 3;
        while (time > 0)
        {
            scoreText.text = time.ToString();
            yield return new WaitForSecondsRealtime(1);
            time--;
        }

        pauseMenu.SetActive(true);
        scoreText.text = $"{scorePlayer} - {scoreAI}";
        Time.timeScale = 1;
    }

    private void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerScored()
    {
        scorePlayer++;
        scoreText.text = $"{scorePlayer} - {scoreAI}";
    }

    public void AIScored()
    {
        scoreAI++;
        scoreText.text = $"{scorePlayer} - {scoreAI}";
    }
    #endregion

    #region Shared methods
    public void PlayClick()
    {
        clickSFX.Play();
    }

    public void PlayStart()
    {
        startSFX.Play();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
    #endregion
}
