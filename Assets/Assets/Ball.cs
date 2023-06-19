using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Score scoreInstance;
    private Rigidbody2D rb2D;
    public int spd;

    private void Start()
    {
        scoreInstance = Score.instance;
        rb2D = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal (player)"))
        {
            scoreInstance.AIScored();
        }
        else if (other.gameObject.CompareTag("Goal (AI)"))
        {
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
}
