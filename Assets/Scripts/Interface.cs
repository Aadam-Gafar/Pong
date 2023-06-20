using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    // Creating a global instance of Score.
    public static Interface instance;

    public int targetFPS;

    private int scorePlayer;
    private int scoreAI;
    public Text scoreText;

    private bool isCounting;
    private bool isPaused;
    public GameObject pauseMenu;
    public GameObject controlsMenu;

    private void Awake()
    {
        CheckInstance();
    }

    void Start()
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

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
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
}
