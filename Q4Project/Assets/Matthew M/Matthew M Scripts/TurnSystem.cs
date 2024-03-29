using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
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
    public TMP_Text PlayerText;
    public GameObject PayerText;
    public TMP_Text EnemyText;
    public GameObject EmemyText;
    public TMP_Text NeutralText;
    public GameObject NewtallText;
    public TMP_Text PlayerSkillText;
    public TMP_Text EnemySkillText;
    public TMP_Text NeutralSkillText;
    public TMP_Text Skill1;
    public TMP_Text Skill2;
    public TMP_Text Skill3;
    public TMP_Text Skill4;
    public GameObject BulletBox;
    public GameObject Heart;
    public GameObject UI;
    public GameObject AttackAnim;
    public GameObject SkillsMenu;
    public PlayerRPG Billy;
    public EnemyRPG badguy;
    public bool cooldown;
    public bool isDead;

    IEnumerator SetupBattle()
    {
        player.GetComponent<PlayerRPG>();
        enemy.GetComponent<EnemyRPG>();
        BulletBox.SetActive(false);
        Heart.SetActive(false);
        AttackAnim.SetActive(false);
        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + ": " + player.GetComponent<PlayerRPG>().currentHP;
        EnemyText.text = enemy.GetComponent<EnemyRPG>().EnemyName + ": " + enemy.GetComponent<EnemyRPG>().enemyHP;
        NeutralText.text = enemy.GetComponent<EnemyRPG>().EnemyName + " Attacked!";

        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(1f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        BulletBox.SetActive(false);
        Heart.SetActive(false);
        UI.SetActive(true);
        AttackAnim.SetActive(false);
        PlayerText.text = player.GetComponent<PlayerRPG>().PlayerName + ": " + player.GetComponent<PlayerRPG>().currentHP;
        NeutralText.text = "Choose an action: ";
    }
    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        bool isDead = enemy.GetComponent<EnemyRPG>().TakeDamage(player.GetComponent<PlayerRPG>().attack);
        EnemyText.text = enemy.GetComponent<EnemyRPG>().EnemyName + ": " + enemy.GetComponent<EnemyRPG>().enemyHP;
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
        SkillsMenu.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Heart.SetActive(true);
        AttackAnim.SetActive(true);
        Heart.GetComponent<BulletBox>().SetHeart();
        yield return new WaitForSeconds(5f);
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
        if (cooldown == false)
        {
            StartCoroutine(PlayerAttack());
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public void ResetCooldown()
    {
        cooldown = false;
    }

    public void OnSkills()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        if (cooldown == false)
        {
            PlayerSkills();
            Invoke("ResetCooldown", 1f);
            cooldown = true;
        }
    }

    public void PlayerSkills()
    {
        UI.SetActive(false);
        NewtallText.SetActive(true);
        EmemyText.SetActive(true);
        PayerText.SetActive(true);
        PlayerSkillText.text = player.GetComponent<PlayerRPG>().PlayerName + ": " + player.GetComponent<PlayerRPG>().currentHP;
        EnemySkillText.text = enemy.GetComponent<EnemyRPG>().EnemyName + ": " + enemy.GetComponent<EnemyRPG>().enemyHP;
        NeutralSkillText.text = "Choose a Skill: ";

        if (Billy.Level < 2)
        {
            Skill1.text = "LOCKED - LEVEL 2";
            Skill2.text = "LOCKED - LEVEL 4";
            Skill3.text = "LOCKED - LEVEL 6";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else if (Billy.Level < 4)
        {
            Skill1.text = "Fresh Corn - 5 IQ";
            Skill2.text = "LOCKED - LEVEL 4";
            Skill3.text = "LOCKED - LEVEL 6";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else if (Billy.Level < 6)
        {
            Skill1.text = "Fresh Corn - 5 IQ";
            Skill2.text = "Billy Brawl - 10 IQ";
            Skill3.text = "LOCKED - LEVEL 6";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else if (Billy.Level < 8)
        {
            Skill1.text = "Fresh Corn - 5 IQ";
            Skill2.text = "Billy Brawl - 10 IQ";
            Skill3.text = "Intimidating Talk - 15 IQ";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else
        {
            Skill1.text = "Fresh Corn - 5 IQ";
            Skill2.text = "Billy Brawl - 10 IQ";
            Skill3.text = "Intimidating Talk - 15 IQ";
            Skill4.text = "Truck of Doom - 20 IQ";
        }
    }

    public void OnCorn()
    {
        if (Billy.Level < 2) 
        { 
            return; 
        }
        if (cooldown == false)
        {
            CornHeal();
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public void CornHeal()
    {
        
    }

    public void OnBrawl()
    {
        if (Billy.Level < 4)
        {
            return;
        }
        if (cooldown == false)
        {
            BillyBrawl();
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public void BillyBrawl()
    {

    }

    public void OnTalk()
    {
        if (Billy.Level < 6)
        {
            return;
        }
        if (cooldown == false)
        {
            MeanTalk();
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public void MeanTalk()
    {

    }

    public void OnTruck()
    {
        if (Billy.Level < 8)
        {
            return;
        }
        if (cooldown == false)
        {
            TruckTime();
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public void TruckTime()
    {

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
        if (cooldown == false)
        {
            StartCoroutine(PlayerFlee());
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
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