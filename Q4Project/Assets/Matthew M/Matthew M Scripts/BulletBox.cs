using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 movement;
    public Vector3 Startingpos;
    public Vector2 MaxPos;
    public Vector2 MinPos;

    void Start()
    {
        SetHeart();
    }

    public void SetHeart()
    {
        transform.position = Startingpos;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (transform.position.x >= MaxPos.x)
        {
            transform.position = new Vector3(MaxPos.x, transform.position.y, 0);
        }
        if (transform.position.y >= MaxPos.y)
        {
            transform.position = new Vector3(transform.position.x, MaxPos.y, 0);
        }
        if (transform.position.x <= MinPos.x)
        {
            transform.position = new Vector3(MinPos.x, transform.position.y, 0);
        }
        if (transform.position.y <= MinPos.y)
        {
            transform.position = new Vector3(transform.position.x, MinPos.y, 0);
        }
        else
        {
            rb.velocity = movement * moveSpeed;
        }

        movement.Normalize();

        rb.velocity = movement * moveSpeed;
    }
}
