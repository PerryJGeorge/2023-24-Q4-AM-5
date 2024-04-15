using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink_Slide : MonoBehaviour
{

    public GameObject player;
    public float a;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player.GetComponent<PlayerCtrl>().enabled = false;
        a = player.GetComponent<Rigidbody2D>().angularDrag;
        player.GetComponent<Rigidbody2D>().angularDrag = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<PlayerCtrl>().enabled = true;
        player.GetComponent<Rigidbody2D>().angularDrag = a;
    }
}
