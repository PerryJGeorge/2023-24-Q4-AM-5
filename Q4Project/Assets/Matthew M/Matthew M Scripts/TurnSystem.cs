using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

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
    public GameObject UI;

    IEnumerator SetupBattle()
    {
        player.GetComponent<PlayerRPG>();
        enemy.GetComponent<EnemyRPG>();
        BulletBox.SetActive(false);
        Heart.SetActive(false);
        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + ": " + player.GetComponent<PlayerRPG>().currentHP;
        EnemyText.text = enemy.GetComponent<EnemyRPG>().EnemyName + ": " + enemy.GetComponent<EnemyRPG>().enemyHP;

        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(1f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        BulletBox.SetActive(false);
        Heart.SetActive(false);
        UI.SetActive(true);
        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + ": " + player.GetComponent<PlayerRPG>().currentHP;
        NeutralText.text = "Choose an action:";
    }
    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        bool isDead = enemy.GetComponent<EnemyRPG>().TakeDamage(player.GetComponent<PlayerRPG>().attack);

        EnemyText.text = enemy.GetComponent<EnemyRPG>().EnemyName + " " + enemy.GetComponent<EnemyRPG>().enemyHP;
        NeutralText.text = "You attacked!";
        yield return new WaitForSeconds(1f);

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
        UI.SetActive(false);
        BulletBox.SetActive(true);
        Heart.SetActive(true);
        yield return new WaitForSeconds(3f);
        //foreach(EnemyProfile emy in EnemiesInBattle)
        //{
        //    int AtkNumber = Random.Range(0, emy.EnemiesAttacks.Length);
        //
        //    Instantiate(emy.EnemiesAttacks[AtkNumber], Vector3.zero, Quaternion.identity);
        //}
        //EnemyAttacks = GameObject.FindGameObjectsWithTag("Enemy");

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

    public void OnSkills()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
    }

    public void OnItem()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
    }

    public void OnFlee()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerFlee());
    }

    IEnumerator PlayerFlee()
    {
        int loser = Random.Range(0, 100);
        NeutralText.text = "You attempt to escape...";
        yield return new WaitForSeconds(2f);
        if (loser > 50 + (2 * player.GetComponent<PlayerRPG>().Eva))
        {
            NeutralText.text = "You escaped the battle!";
            SceneManager.LoadScene("Ian Test Scene");
        }
        else
        {
            NeutralText.text = "You can't escape!";
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
    }

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }
}