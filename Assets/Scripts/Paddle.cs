using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public int spd = 10;
    public bool isAI;
    public float range;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        KeyboardControls();
        //MobileControls();
        Clamping();
    }

    private void KeyboardControls()
    {
        float moveDir;
        if (!isAI)
        {
            moveDir = Input.GetAxis("Horizontal");
        }
        else
        {
            GameObject ball = GameObject.Find("Ball");
            if (ball.transform.position.x > transform.position.x)
            {
                moveDir = 1;
            }
            else if (ball.transform.position.x < transform.position.x)
            {
                moveDir = -1;
            }
            else
            {
                moveDir = 0;
            }
        }
        rb2D.velocity = new Vector2(moveDir * spd, 0);
    }

    private void MobileControls()
    {
        float moveDir;
        if (!isAI)
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
            GameObject ball = GameObject.Find("Ball");
            if (ball.transform.position.x > transform.position.y)
            {
                moveDir = 1;
            }
            else
            {
                moveDir = -1;
            }
        }
        rb2D.velocity = new Vector2(moveDir * spd, 0);
    }

    private void Clamping()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -range, range);
        transform.position = position;
    }
}
