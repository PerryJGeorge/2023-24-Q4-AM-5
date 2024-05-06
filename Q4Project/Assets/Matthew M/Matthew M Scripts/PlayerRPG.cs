using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : MonoBehaviour
{
    public static string PlayerName = "Billy";
    public static int MaxHP = 15;
    public static int currentHP = 15;
    public static int currentIQ = 5;
    public static int maxIQ = 5;
    public static int strength = 2;
    public static int defense = 1;
    public static int intellect = 3;
    public static int Eva = 2;
    public static int Luck = 3;
    public static int EXP = 0;
    public static int Level = 1;

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
