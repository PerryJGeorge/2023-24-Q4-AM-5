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
    public int strength;
    public int defense;
    public int intellect;
    public int EXP;
    public int Dollars;
    public int Eva;
    public int Level = 1;

    public bool TakeDamage(int enemydamage)
    {
        currentHP -= enemydamage - defense;
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > MaxHP)
        {
            currentHP = MaxHP;
        }
    }
}
