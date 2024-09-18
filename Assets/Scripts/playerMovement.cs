using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;

    private Rigidbody2D rb;
    private Vector2 playerMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isAI)
        {
            AIControl();
        }
        else
        {
            PlayerControl();
        }
    }

    private void PlayerControl()
    {
        playerMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void AIControl()
    {
        float xDirection = 0;
        float yDirection = 0;

        if(ball.transform.position.y > transform.position.y + 0.5f)
        {
            yDirection = 1;
        }
        else if(ball.transform.position.y < transform.position.y - 0.5f)
        {
            yDirection = -1;
        }

        if(ball.transform.position.x > transform.position.x + 0.5f)
        {
            xDirection = 1;
        }
        else if(ball.transform.position.x < transform.position.x - 0.5f)
        {
            xDirection = -1;
        }

        playerMove = new Vector2(xDirection, yDirection);
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;
    }
}
