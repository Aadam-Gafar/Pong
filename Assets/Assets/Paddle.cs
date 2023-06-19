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
        Movement();
        Clamping();
    }

    private void Movement()
    {
        float moveBtn;
        if (!isAI)
        {
            moveBtn = Input.GetAxis("Vertical");
        }
        else
        {
            GameObject ball = GameObject.Find("Ball");
            if (ball.transform.position.y > transform.position.y)
            {
                moveBtn = 1;
            }
            else
            {
                moveBtn = -1;
            }
        }
        rb2D.velocity = new Vector2(0, moveBtn * spd);
    }

    private void Clamping()
    {
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, -range, range);
        transform.position = position;
    }
}
