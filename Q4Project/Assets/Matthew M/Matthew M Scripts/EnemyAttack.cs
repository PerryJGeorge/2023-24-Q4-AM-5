using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerRPG target;
    public EnemyRPG enemyattack;
    public GameObject Heart;
    public float IFrames = 0.5f;

    public IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if (Heart.GetComponent<BoxCollider2D>().enabled)
        {
            target.TakeDamage(enemyattack.enemydamage);
            Heart.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(IFrames);
            Heart.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}