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
        scoreText.text = $"{scorePlayer} - {scoreAI}";
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
