using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Corrected typo in GetComponent<Animator>()
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

            animator.SetFloat("XInput", moveInput.x);
            animator.SetFloat("YInput", moveInput.y);
    }

    // Update is called once per frame
    void Update()
    {
        speedX = moveInput.x * movSpeed; // Use moveInput.x instead of Input.GetAxisRaw("Horizontal")
        speedY = moveInput.y * movSpeed; // Use moveInput.y instead of Input.GetAxisRaw("Vertical")
        rb.velocity = new Vector2(speedX, speedY);
    }
}
