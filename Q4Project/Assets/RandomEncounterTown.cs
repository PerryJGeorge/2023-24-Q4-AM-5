using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterTown : MonoBehaviour
{
    public flick loadbattlescene;
    public GameObject player;

    void Start()
    {
        StartCoroutine(CheckForScene());
    }

    IEnumerator CheckForScene()
    {
        yield return new WaitForSeconds(1f);
        if (Random.Range(1, 101) <= 15)
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x > 0 || player.GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                loadbattlescene.UniqueExit();
            }
        }
        StartCoroutine(CheckForScene());
    }
}
