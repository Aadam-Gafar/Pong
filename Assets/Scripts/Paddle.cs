using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public int spd;
    public float reaction;
    public float returnForce;
    public float angleForce;
    public bool isAI;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        AIMovement();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Adds minor horizontal movement to prevent perpetual vertical motion
        if (gameObject.tag == "Paddle (player)" || gameObject.tag == "Paddle (AI)")
        {
            float randomForce = Random.Range(-0.5f, 0.5f);
            collision.rigidbody.AddForce(new Vector2(randomForce, 0), ForceMode2D.Impulse);
        }

        // Adds impulse to ball when it hits paddle, thereby speeding up on each rally
        switch (gameObject.tag)
        {
            case "Paddle (player)":
                collision.rigidbody.AddForce(new Vector2(0, returnForce), ForceMode2D.Impulse);
                break;
            case "Paddle (AI)":
                collision.rigidbody.AddForce(new Vector2(0, -returnForce), ForceMode2D.Impulse);
                break ;
        }

        // Allows the player to control ball trajectory with paddle edges
        if (gameObject.CompareTag("Paddle (player)") || gameObject.CompareTag("Paddle (AI)"))
        {
            float difference = collision.transform.position.x - transform.position.x;
            difference /= gameObject.GetComponent<Collider2D>().bounds.size.x / 2;
            collision.rigidbody.AddForce(new Vector2(difference * angleForce, 0), ForceMode2D.Impulse);
        }
    }

    private void KeyboardControls()
    {
        float moveDir;
        if (!isAI)
        {
            moveDir = Input.GetAxis("Horizontal");
            rb2D.velocity = new Vector2(moveDir * spd, 0);
        }
    }

    private void MobileControlsV1()
    {
        float moveDir;
        if (!isAI)
        {
            if(Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    moveDir = -1;
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    moveDir = 1;
                }
                else
                {
                    moveDir = 0;
                }
            }
            else
            {
                moveDir = 0;
            }
            rb2D.velocity = new Vector2(moveDir * spd, 0);
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
            float newX = Mathf.Lerp(transform.position.x, ball.transform.position.x, reaction);
            transform.position = new Vector2(newX, transform.position.y);
        }
    }
}
