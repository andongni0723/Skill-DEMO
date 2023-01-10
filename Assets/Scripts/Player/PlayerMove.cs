using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    private Rigidbody2D rb;

    public float speed;

    private float movementX;
    private float movementY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(movementX * speed * Time.deltaTime, movementY * speed * Time.fixedDeltaTime);
    }
}
