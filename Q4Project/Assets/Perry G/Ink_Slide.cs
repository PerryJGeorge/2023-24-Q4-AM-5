using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink_Slide : MonoBehaviour
{

    public GameObject player;
    public float a;
    private void OnTriggerEnter2D(Collider2D other)
    {
        a = player.GetComponent<Rigidbody2D>().sharedMaterial.friction;
        player.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<Rigidbody2D>().sharedMaterial.friction = a;
    }
}
