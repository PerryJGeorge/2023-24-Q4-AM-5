using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : MonoBehaviour
{
    public string PlayerName;
    public int MaxHP;
    public int currentHP;
    public int currentIQ;
    public int maxIQ;
    public int attack;
    public int defense;
    public int EXP;
    public int Dollars;
    public int Eva;
    public int Level = 1;

    public bool TakeDamage(int enemydamage)
    {
        currentHP -= enemydamage;
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
