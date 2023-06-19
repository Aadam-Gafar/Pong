using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Score scoreInstance;
    private Rigidbody2D rb2D;
    public int spd;
    public int min;
    public int max;

    public AudioSource winSFX;
    public AudioSource bounceSFX;
    public AudioSource lossSFX;

    public int xBound;
    public int yBound;

    private void Start()
    {
        scoreInstance = Score.instance;
        rb2D = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Imposes a max speed limit (both axes) on the ball
        if (rb2D.velocity.magnitude > max)
        {
            rb2D.velocity = rb2D.velocity.normalized * max;
        }

        // Imposes a min speed limit (y-axis) on the ball
        if (rb2D.velocity.y > 0 && rb2D.velocity.y < min)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, min);
        }
        else if (rb2D.velocity.y < 0 && rb2D.velocity.y < min)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, -min);
        }

        bounceSFX.Play();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        StartCoroutine(CheckBounds());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal (player)"))
        {
            lossSFX.Play();
            scoreInstance.AIScored();
        }
        else if (other.gameObject.CompareTag("Goal (AI)"))
        {
            winSFX.Play();
            scoreInstance.PlayerScored();
        }
        ResetBall();
    }

    private void Launch()
    {
        // If direction is 0, set it to -1. Else, set it to 1.
        int xDir = Random.Range(0, 2) == 0 ? -1 : 1;
        int yDir = Random.Range(0, 2) == 0 ? -1 : 1;
        rb2D.velocity = new Vector2(spd * xDir, spd * yDir);
    }

    private void ResetBall()
    {
        rb2D.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        Launch();
    }

    // Resets any balls that escape the level
    private IEnumerator CheckBounds()
    {
        yield return new WaitForSeconds(5);
        if (transform.position.x > xBound || transform.position.x < -xBound)
        {
            ResetBall();
        }
        else if (transform.position.y > yBound || transform.position.y < -yBound)
        {
            ResetBall();
        }
    }
}
