using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Creating a global instance of Score.
    public static Score instance;

    public int targetFPS;

    private int scorePlayer;
    private int scoreAI;
    public Text scoreText;
    private bool isPaused;

    private void Awake()
    {
        CheckInstance();
        Pause();
        scoreText.text = "TAP TO BEGIN";
    }
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;

        scorePlayer = 0;
        scoreAI = 0;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }

        if (isPaused && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Resume());
        }
    }

    private void Pause()
    {
        scoreText.text = "PAUSED";
        Time.timeScale = 0;
        isPaused = true;
    }

    private IEnumerator Resume()
    {
        int time = 3;
        while (time > 0)
        {
            scoreText.text = time.ToString();
            yield return new WaitForSecondsRealtime(1);
            time--;
        }

        scoreText.text = $"{scorePlayer} - {scoreAI}";
        Time.timeScale = 1;
        isPaused = false;
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
