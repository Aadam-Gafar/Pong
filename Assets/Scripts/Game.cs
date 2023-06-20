using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // Creating a global instance of Game.
    public static Game instance;

    [HideInInspector] public float screenWidth;
    [HideInInspector] public float screenHeight;

    [HideInInspector] public float wallThickness;
    public float wallScale;
    public GameObject leftWall;
    public GameObject rightWall;

    [HideInInspector] public float goalThickness;
    public float goalScale;
    public GameObject goalPlayer;
    public GameObject goalAI;

    [HideInInspector] public float paddleHeight;
    [HideInInspector] public float paddleWidth;
    public float paddleSizeAI;
    public GameObject paddlePlayer;
    public GameObject paddleAI;

    public float ballScale;
    public GameObject ball;

    // Easy difficulty
    public float paddleSizeE;
    public float spdLimE;
    public float rtnForceE;
    public float skillLvlE;

    // Medium difficulty
    public float paddleSizeM;
    public float spdLimM;
    public float rtnForceM;
    public float skillLvlM;

    // Hard difficulty
    public float paddleSizeH;
    public float spdLimH;
    public float rtnForceH;
    public float skillLvlH;

    private void Awake()
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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            CalculateScreen();
            PositionWalls();
            PositionGoals();
            PositionPaddles();
            ScaleItems();
        }
    }

    private void CalculateScreen()
    {
        // Converts screen dimensions in pixels to transform units
        Camera mainCam = Camera.main;
        Vector2 screenDim = mainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenWidth = screenDim.x * 2;
        screenHeight = screenDim.y * 2;
    }

    private void PositionWalls()
    {
        wallThickness = screenWidth * wallScale;

        leftWall.transform.localScale = new Vector2(wallThickness, screenHeight);
        rightWall.transform.localScale = new Vector2(wallThickness, screenHeight);
        
        leftWall.transform.position = new Vector2(-screenWidth/2 - wallThickness/2, 0);
        rightWall.transform.position = new Vector2(screenWidth/2 + wallThickness/2, 0);
    }

    private void PositionGoals()
    {
        goalThickness = screenWidth * goalScale;

        goalPlayer.transform.localScale = new Vector2(screenWidth, goalThickness);
        goalAI.transform.localScale = new Vector2(screenWidth, goalThickness);

        goalPlayer.transform.position = new Vector2(0, -screenHeight/2);
        goalAI.transform.position = new Vector2(0, screenHeight/2);
    }

    private void PositionPaddles()
    {
        paddlePlayer.transform.position = new Vector2(0, -screenHeight / 2 + goalThickness/2);
        paddleAI.transform.position = new Vector2(0, screenHeight / 2 - goalThickness/2);
    }

    private void ScaleItems()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        float paddleHeight = screenWidth * paddleSizeAI / 10;

        switch (difficulty)
        {
            case 0:
                paddlePlayer.transform.localScale = new Vector2(screenWidth * paddleSizeE, paddleHeight);
                paddleAI.transform.localScale = new Vector2(screenWidth * paddleSizeAI, paddleHeight);
                break;
            case 1:
                paddlePlayer.transform.localScale = new Vector2(screenWidth * paddleSizeM, paddleHeight);
                paddleAI.transform.localScale = new Vector2(screenWidth * paddleSizeAI, paddleHeight);
                break;
            case 2:
                paddlePlayer.transform.localScale = new Vector2(screenWidth * paddleSizeH, paddleHeight);
                paddleAI.transform.localScale = new Vector2(screenWidth * paddleSizeAI, paddleHeight);
                break;
        }

        float ballDim = screenWidth * ballScale;
        ball.transform.localScale = new Vector2(ballDim, ballDim);
    }
}
