using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float skillLvl;
    public float speedLim;
    public float rtnForce;
    public float angleForce;
    public bool isAI;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        SetDifficulty();
    }

    void Update()
    {
        Movement();
        AIMovement();
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Adds minor horizontal movement to prevent perpetual vertical motion
        if (gameObject.tag == "Paddle (player)" || gameObject.tag == "Paddle (AI)")
        {
            float randomForce = Random.Range(-0.5f, 0.5f);
            collision.rigidbody.AddForce(new Vector2(randomForce, 0), ForceMode2D.Impulse);
        }

        // Adds impulse to ball when it hits paddle, thereby speeding up on each rally
        if (collision.rigidbody.velocity.magnitude < speedLim)
        {
            Debug.Log("Inside if.");
            switch (gameObject.tag)
            {
                case "Paddle (player)":
                    collision.rigidbody.AddForce(new Vector2(0, rtnForce), ForceMode2D.Impulse);
                    break;
                case "Paddle (AI)":
                    Debug.Log("In impulse script.");
                    collision.rigidbody.AddForce(new Vector2(0, -rtnForce), ForceMode2D.Impulse);
                    break;
            }
        }

        // Allows the player to control ball trajectory with paddle edges
        if (gameObject.CompareTag("Paddle (player)") || gameObject.CompareTag("Paddle (AI)"))
        {
            float difference = collision.transform.position.x - transform.position.x;
            difference /= gameObject.GetComponent<Collider2D>().bounds.size.x / 2;
            collision.rigidbody.AddForce(new Vector2(difference * angleForce, 0), ForceMode2D.Impulse);
        }
    }

    private void Movement()
    {
        if(!isAI)
        {
            // Find mouse position in world units (as opposed to pixels)
            Vector2 mousePosition = Input.mousePosition;
            Vector2 mouseTransform = Camera.main.ScreenToWorldPoint(mousePosition);

            // Lerping paddle's x-position to the mouse's x-position
            float newX = Mathf.Lerp(transform.position.x, mouseTransform.x, 1);
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    private void AIMovement()
    {
        if(isAI)
        {
            GameObject ball = GameObject.Find("Ball");
            float newX = Mathf.Lerp(transform.position.x, ball.transform.position.x, skillLvl);
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    private void SetDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        switch (difficulty)
        {
            case 0:
                skillLvl = Game.instance.skillLvlE;
                speedLim = Game.instance.spdLimE;
                rtnForce = Game.instance.rtnForceE;
                break;
            case 1:
                skillLvl = Game.instance.skillLvlM;
                speedLim = Game.instance.spdLimM;
                rtnForce = Game.instance.rtnForceM;
                break;
            case 2:
                skillLvl = Game.instance.skillLvlH;
                speedLim = Game.instance.spdLimH;
                rtnForce = Game.instance.rtnForceH;
                break;
        }
    }
}
