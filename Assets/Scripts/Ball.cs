using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Interface scoreInstance;
    private Rigidbody2D rb2D;

    private int spdG;
    public int spdE;
    public int spdM;
    public int spdH;

    private int maxG;
    public int maxE;
    public int maxM;
    public int maxH;


    public AudioSource winSFX;
    public AudioSource bounceSFX;
    public AudioSource lossSFX;

    private void Start()
    {
        scoreInstance = Interface.instance;
        rb2D = GetComponent<Rigidbody2D>();
        
        SetDifficulty();
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Imposes a max speed limit (both axes) on the ball
        if (rb2D.velocity.magnitude > maxG)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxG;
        }

        // Imposes a min speed limit (y-axis) on the ball
        if (rb2D.velocity.y > 0 && rb2D.velocity.y < spdG)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, spdG);
        }
        else if (rb2D.velocity.y < 0 && rb2D.velocity.y < spdG)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, -spdG);
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
        rb2D.velocity = new Vector2(spdG * xDir, spdG * yDir);
    }

    private void ResetBall()
    {
        rb2D.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        Launch();
    }

    private IEnumerator CheckBounds()
    {
        // Resets any balls that escape the level
        float xBound = Game.instance.screenWidth;
        float yBound = Game.instance.screenHeight;

        yield return new WaitForSeconds(5);
        if (transform.position.x > xBound/2 || transform.position.x < -xBound/2)
        {
            ResetBall();
        }
        else if (transform.position.y > yBound/2 || transform.position.y < -yBound/2)
        {
            ResetBall();
        }
    }

    private void SetDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");

        switch (difficulty)
        {
            case 0:
                spdG = spdE;
                maxG = maxE;
                break;
            case 1:
                spdG = spdM;
                maxG = maxM;
                break;
            case 2:
                spdG = spdH;
                maxG = maxH;
                break;
        }
    }
}
