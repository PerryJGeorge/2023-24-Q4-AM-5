using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : MonoBehaviour
{
    public string PlayerName;
    public int MaxHP;
    public int currentHP;
    public int attack;
    public int defense;
    public int EXP;
    public int Dollars;
    public int Eva;
    private int Level = 1;
    public EnemyAttack atak;

    public bool TakeDamage(int enemydamage)
    {
        if (atak.isFrames)
        {
            currentHP -= enemydamage;
        }
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
