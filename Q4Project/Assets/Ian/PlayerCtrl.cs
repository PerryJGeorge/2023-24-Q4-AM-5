using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{


    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb; 
    private Animator animator;
    p

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Ainmator>();
    }

    public void OnMove(InputValue value)
    {
        animator.SetFloat("XInput", moveInput.x);
        animator.SetFloat("XInput", moveInput.x);
    }

    // Update is called once per frame
    void Update()
    {

        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);


    }
}
