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
    public KeyCode Attackbutton;
    public KeyCode HealButton;
    IEnumerator SetupBattle()
    {
        player.GetComponent<PlayerRPG>();
        enemy.GetComponent<script2>();
        state = BattleState.PLAYERTURN;
        PlayerTurn();

        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + " " + player.GetComponent<PlayerRPG>().currentHP;
        EnemyText.text = enemy.GetComponent<script2>().EnemyName + " " + enemy.GetComponent<script2>().enemyHP;

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        NeutralText.text = "Choose an action.";
        if (Input.GetKeyDown(Attackbutton))
        {
            OnAttack();
        }
        else if (Input.GetKeyDown(HealButton))
        {

        }
    }
    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        bool isDead = enemy.GetComponent<script2>().TakeDamage(player.GetComponent<PlayerRPG>().attack);

        EnemyText.text = enemy.GetComponent<script2>().EnemyName + " " + enemy.GetComponent<script2>().enemyHP;
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
        EnemyText.text = enemy.GetComponent<script2>().EnemyName + " Attacked!";
        yield return new WaitForSeconds(1f);
        bool isDead = player.GetComponent<PlayerRPG>().TakeDamage(enemy.GetComponent<script2>().enemydamage);
        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + " " + player.GetComponent<PlayerRPG>().currentHP;
        yield return new WaitForSeconds(1f);
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
