using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHP2 : MonoBehaviour
{
    public EnemyRPG enemy;

    private void Awake()
    {
        enemy.enemyHP = 12;
    }
}
