using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }


public class TurnSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public BattleState state;
    public Text PlayerText;
    public Text EnemyText;
    public Text NeutralText;
    public bool isDead;
    //public EnemyProfile[] EnemiesInBattle;
    public GameObject[] EnemyAttacks;
    public GameObject BulletBox;
    public GameObject Heart;

    IEnumerator SetupBattle()
    {
        player.GetComponent<PlayerRPG>();
        enemy.GetComponent<EnemyRPG>();
        state = BattleState.PLAYERTURN;
        PlayerTurn();

        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + " " + player.GetComponent<PlayerRPG>().currentHP;
        EnemyText.text = enemy.GetComponent<EnemyRPG>().EnemyName + " " + enemy.GetComponent<EnemyRPG>().enemyHP;

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        BulletBox.SetActive(false);
        Heart.SetActive(false);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        NeutralText.text = "Choose an action.";
    }
    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        bool isDead = enemy.GetComponent<EnemyRPG>().TakeDamage(player.GetComponent<PlayerRPG>().attack);

        EnemyText.text = enemy.GetComponent<EnemyRPG>().EnemyName + " " + enemy.GetComponent<EnemyRPG>().enemyHP;
        NeutralText.text = "You attacked!";

        if (isDead)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        //foreach(EnemyProfile emy in EnemiesInBattle)
        //{
        //    int AtkNumber = Random.Range(0, emy.EnemiesAttacks.Length);
        //
        //    Instantiate(emy.EnemiesAttacks[AtkNumber], Vector3.zero, Quaternion.identity);
        //}
        EnemyAttacks = GameObject.FindGameObjectsWithTag("Enemy");
        if (isDead)
        {
            state = BattleState.LOSE;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WIN)
        {
            NeutralText.text = "You Win!";
        }
        else if (state == BattleState.LOSE)
        {
            NeutralText.text = "You Lose!";
        }
    }

    public void OnAttack()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());

    }
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }
}
