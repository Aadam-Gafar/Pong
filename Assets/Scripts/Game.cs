using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

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
    public float paddleScale;
    public GameObject paddlePlayer;
    public GameObject paddleAI;

    public float ballScale;
    public GameObject ball;

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
        CalculateScreen();
        PositionWalls();
        PositionGoals();
        PositionPaddles();
        ScaleItems();
    }

    private void Update()
    {
        CalculateScreen();
        PositionWalls();
        PositionGoals();
        PositionPaddles();
        ScaleItems();
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
        float ballDim = screenWidth * ballScale;
        ball.transform.localScale = new Vector2(ballDim, ballDim);

        float paddleWidth = screenWidth * paddleScale;
        float paddleHeight = screenWidth * paddleScale / 10;
        paddlePlayer.transform.localScale = new Vector2(paddleWidth, paddleHeight);
        paddleAI.transform.localScale = new Vector2(paddleWidth, paddleHeight);
    }
}
