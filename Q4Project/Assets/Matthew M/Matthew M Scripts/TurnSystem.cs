using JetBrains.Annotations;
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
    public List<string> EnemyType;
    public List<GameObject> AttackList;
    public GameObject SkillsMenu;
    public PlayerRPG Billy;
    public EnemyRPG badguy;
    public bool cooldown;
    public bool isDead;
    public flick scenechange;

    IEnumerator SetupBattle()
    {
        player.GetComponent<PlayerRPG>();
        enemy.GetComponent<EnemyRPG>();
        BulletBox.SetActive(false);
        Heart.SetActive(false);
        for (int i = 0; i < AttackList.Count; i++)
        {
            if (AttackList[i].activeSelf == true)
            {
                AttackList[i].SetActive(false);
            }
        }
        if(PlayerRPG.currentHP > 0)
        {
            PlayerText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP";
        }
        else
        {
            PlayerText.text = PlayerRPG.PlayerName + ": 0 HP";
        }
        PlayerText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        if (badguy.enemyHP > 0)
        {
            EnemyText.text = badguy.EnemyName + ": " + badguy.enemyHP + " HP";
        }
        else
        {
            EnemyText.text = badguy.EnemyName + ": 0 HP";
        }
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
        for (int i = 0; i < AttackList.Count;  i++)
        {
            if (AttackList[i].activeSelf == true)
            {
                AttackList[i].SetActive(false);
            }
        }
        SkillsMenu.SetActive(false);
        PlayerText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        if (badguy.enemyHP > 0)
        {
            EnemyText.text = badguy.EnemyName + ": " + badguy.enemyHP + " HP";
        }
        else
        {
            EnemyText.text = badguy.EnemyName + ": 0 HP";
        }
        NeutralText.text = "Choose an action: ";
    }
    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        bool isDead = enemy.GetComponent<EnemyRPG>().TakeDamage(PlayerRPG.strength);
        if (badguy.enemyHP > 0)
        {
            EnemyText.text = badguy.EnemyName + ": " + badguy.enemyHP + " HP";
        }
        else
        {
            EnemyText.text = badguy.EnemyName + ": 0 HP";
        }
        NeutralText.text = "You attacked!";
        yield return new WaitForSeconds(1f);

        if (badguy.enemyHP <= 0)
        {
            state = BattleState.WIN;
            StartCoroutine(EndBattle());
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
        Heart.GetComponent<BoxCollider2D>().enabled = true;
        int AttackChosen = Random.Range(0, AttackList.Count);
        AttackList[AttackChosen].SetActive(true);
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

    IEnumerator EndBattle()
    {
        if (state == BattleState.WIN)
        {
            NeutralText.text = "You Win!";
            NeutralSkillText.text = "You Win!";
            PlayerRPG.EXP += 5;
            if (PlayerRPG.EXP > 20)
            {
                yield return new WaitForSeconds(2f);
                NeutralText.text = "Level Up!";
                NeutralSkillText.text = "Level Up!";
                PlayerRPG.MaxHP += 5;
                PlayerRPG.currentHP = PlayerRPG.MaxHP;
                PlayerRPG.maxIQ += 2;
                PlayerRPG.currentIQ = PlayerRPG.maxIQ;
                PlayerRPG.strength += 1;
                PlayerRPG.defense += 1;
                PlayerRPG.intellect += 1;
                PlayerRPG.Eva += 1;
                PlayerRPG.Luck += 1;
                PlayerRPG.Level += 1;
                PlayerRPG.EXP = 0;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
            scenechange.UniqueExit();
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
        SkillsMenu.SetActive(true);
        NewtallText.SetActive(true);
        EmemyText.SetActive(true);
        PayerText.SetActive(true);
        PlayerSkillText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        if (badguy.enemyHP > 0)
        {
            EnemySkillText.text = badguy.EnemyName + ": " + badguy.enemyHP + " HP";
        }
        else
        {
            EnemySkillText.text = badguy.EnemyName + ": 0 HP";
        }
        NeutralSkillText.text = "Choose a Skill: ";

        if (PlayerRPG.Level < 2)
        {
            Skill1.text = "LOCKED - LEVEL 2";
            Skill2.text = "LOCKED - LEVEL 4";
            Skill3.text = "LOCKED - LEVEL 6";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else if (PlayerRPG.Level < 4)
        {
            Skill1.text = "Fresh Corn - 5 IQ";
            Skill2.text = "LOCKED - LEVEL 4";
            Skill3.text = "LOCKED - LEVEL 6";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else if (PlayerRPG.Level < 6)
        {
            Skill1.text = "Fresh Corn - 5 IQ";
            Skill2.text = "Billy Brawl - 10 IQ";
            Skill3.text = "LOCKED - LEVEL 6";
            Skill4.text = "LOCKED - LEVEL 8";
        }
        else if (PlayerRPG.Level < 8)
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
        if (PlayerRPG.Level < 2) 
        { 
            return; 
        }
        if (PlayerRPG.currentIQ < 5)
        {
            return;
        }
        if (cooldown == false)
        {
            StartCoroutine(CornHeal());
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public IEnumerator CornHeal()
    {
        NeutralSkillText.text = "Billy healed " + PlayerRPG.intellect + " HP!";
        PlayerRPG.currentIQ -= 5;
        PlayerSkillText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        yield return new WaitForSeconds(1f);
        Billy.Heal(PlayerRPG.intellect);
        PlayerSkillText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyTurn());
    }

    public void OnBrawl()
    {
        if (PlayerRPG.Level < 4)
        {
            return;
        }
        if (PlayerRPG.currentIQ < 10)
        {
            return;
        }
        if (cooldown == false)
        {
            StartCoroutine(BillyBrawl());
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public IEnumerator BillyBrawl()
    {
        NeutralSkillText.text = "BILLY BRAWL!";
        PlayerRPG.currentIQ -= 10;
        PlayerSkillText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        bool isDead = enemy.GetComponent<EnemyRPG>().TakeDamage(PlayerRPG.strength * 2);
        yield return new WaitForSeconds(1f);
        NeutralSkillText.text = "Billy did " + (PlayerRPG.strength * 2) + " Damage!";
        if (badguy.enemyHP > 0)
        {
            EnemySkillText.text = badguy.EnemyName + ": " + badguy.enemyHP + " HP";
        }
        else
        {
            EnemySkillText.text = badguy.EnemyName + ": 0 HP";
        }
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

    public void OnTalk()
    {
        if (PlayerRPG.Level < 6)
        {
            return;
        }
        if (PlayerRPG.currentIQ < 15) 
        {
            return;
        }
        if (cooldown == false)
        {
            StartCoroutine(MeanTalk());
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public IEnumerator MeanTalk()
    {
        NeutralSkillText.text = "Billy used mean talk!";
        PlayerRPG.currentIQ -= 15;
        PlayerSkillText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        yield return new WaitForSeconds(1f);
        int meanie = Random.Range(0, 100);
        NeutralSkillText.text = "\"I hope you don't have that great of a day!\"";
        yield return new WaitForSeconds(2f);
        if ((meanie + (PlayerRPG.Luck / 2)) < 90 )
        {
            NeutralSkillText.text = "The enemy was scared and escaped!";
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("fE");
        }
        else
        {
            NeutralSkillText.text = "The enemy was not amused!";
            yield return new WaitForSeconds(1f);
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnTruck()
    {
        if (PlayerRPG.Level < 8)
        {
            return;
        }
        if (PlayerRPG.currentIQ < 20)
        {
            return;
        }
        if (cooldown == false)
        {
            StartCoroutine(TruckTime());
            Invoke("ResetCooldown", 3f);
            cooldown = true;
        }
    }

    public IEnumerator TruckTime()
    {
        NeutralSkillText.text = "Billy calls his Truck!";
        PlayerRPG.currentIQ -= 20;
        PlayerSkillText.text = PlayerRPG.PlayerName + ": " + PlayerRPG.currentHP + " HP | " + PlayerRPG.currentIQ + " IQ";
        yield return new WaitForSeconds(1f);
        NeutralSkillText.text = "It does massive damage!";
        bool isDead = enemy.GetComponent<EnemyRPG>().TakeDamage(PlayerRPG.strength * 5);
        if (badguy.enemyHP > 0)
        {
            EnemySkillText.text = badguy.EnemyName + ": " + badguy.enemyHP + " HP";
        }
        else
        {
            EnemySkillText.text = badguy.EnemyName + ": 0 HP";
        }
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

    public void OnBack()
    {
        SkillsMenu.SetActive(false);
        UI.SetActive(true);
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
        if (loser < 50 + (2 * PlayerRPG.Eva))
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