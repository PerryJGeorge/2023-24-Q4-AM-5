using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push_button : MonoBehaviour
{
    
    public enum direction
    {
        up, down, left, right
    }
    public KeyCode Interact;
    public float interactRange = 30f;
    public LayerMask Interactable;
    public static direction move;
    public GameObject player;
    public static int dir;
    void Update()
    {
        if (player.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            move = direction.right;
            dir = 1;

        }
        if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            move = direction.left;
            dir = -1;
        }
        if (player.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            move = direction.up;
            dir = 2;
        }
        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            move = direction.down;
            dir = -2;
        }
        if (Input.GetKeyDown(Interact))
        {

            Collider2D[] collideObject = Physics2D.OverlapCircleAll(transform.position, interactRange, Interactable);
            foreach (Collider2D interactObject in collideObject)
            {
                if (interactObject.gameObject.TryGetComponent(out Block_Push pushScript))
                {
                    pushScript.MoveMe();
                }
            }
        }

    }
}
