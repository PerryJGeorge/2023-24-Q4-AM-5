using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public PlayerRPG target;
    public EnemyRPG enemyattack;
    public GameObject Heart;
    public bool isFrames = false;
    public float IFrames;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        target.TakeDamage(enemyattack.enemydamage);
    }
}