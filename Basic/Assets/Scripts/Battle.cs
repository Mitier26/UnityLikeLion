using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Battle : MonoBehaviour
{
    private Character[] characters;
    private Skill[] skills;
    private int[] turnOrder;
    private string[] battleLog = new string[10];
    private int logIndex = 0;

    public void test()
    {
        InitializeChracters();
        InitializeSkills();
        InitializeTurnOrder();
        StartBattle();
    }

    void InitializeChracters()
    {
        characters = new Character[]
        {
            new Character("Warrior", 100, 15, 10, true), 
            new Character("Mage", 70, 20, 5, true),
            new Character("Healer", 80, 10, 8, true),
            new Character("Goblin", 50, 12, 6, false),
            new Character("Orc", 80, 18, 12, false), 
            new Character("Dark Mage", 60, 25, 7, false)
        };
    }

    void InitializeSkills()
    {
        skills = new Skill[]
        {
            new Skill("Basic Attack", 10, TargetType.SingleEnemy),
            new Skill("Heal", 15, TargetType.SingleAlly),
            new Skill("Fireball", 25, TargetType.SingleEnemy),
            new Skill("Group Heal", 10, TargetType.AllAllies),
            new Skill("Whirlwind", 15, TargetType.AllEnemies)
        };
    }

    void InitializeTurnOrder()
    {
        turnOrder = Enumerable.Range(0, characters.Length).OrderBy(x => UnityEngine.Random.value).ToArray();
    }

    void StartBattle()
    {
        Debug.Log("전투 시작");
        for (int turn = 0; turn < 10; turn++)
        {
            Debug.Log($"Turn {turn + 1}");
            for (int i = 0; i < turnOrder.Length; i++)
            {
                int charIndex = turnOrder[i];
                Character actor = characters[charIndex];
                if (actor.HP > 0)
                {
                    PerformAction(actor);
                }
            }

            if (CheckBattleEnd()) break;
        }

        PrintBattleLog();
    }

    void PerformAction(Character actor)
    {
        Skill selectedSkill = skills[UnityEngine.Random.Range(0, skills.Length)];
        Character target = SelectTarget(selectedSkill.Target, actor.IsPlayer);

        if (target != null)
        {
            int damage = CalculateDamage(actor, target, selectedSkill);
            target.HP -= damage;
            
            string actionLog = $"{actor.Name}이(가) {target.Name}에게 {selectedSkill.Name}을 사용. {damage} 데미지!";
            AddToBattleLog(actionLog);
            Debug.Log(actionLog);
        }
        

        
        
        
    }

    Character SelectTarget(TargetType targetType, bool isPlayerAction)
    {
        Character[] possibleTargets = characters.Where(c => c.IsPlayer != isPlayerAction && c.HP > 0).ToArray();
        if (possibleTargets.Length == 0) return null;
        return possibleTargets[UnityEngine.Random.Range(0, possibleTargets.Length)];
    }

    int CalculateDamage(Character attacker, Character defender, Skill skill)
    {
        int baseDamage = attacker.Attack + skill.Power - defender.Defense;
        return Mathf.Max(0, baseDamage);
    }

    bool CheckBattleEnd()
    {
        bool playerAlive = characters.Any(c => c.IsPlayer && c.HP > 0);
        bool enemiesAlive = characters.Any(c => !c.IsPlayer && c.HP > 0);
        
        if (!playerAlive)
        {
            Debug.Log("적이 승리했다");
            return true;
        }

        if (!enemiesAlive)
        {
            Debug.Log("플레이어가 승리했다.");
            return true;
        }

        return false;
    }

    void AddToBattleLog(string log)
    {
        battleLog[logIndex] = log;
        logIndex = (logIndex + 1) % battleLog.Length;
    }

    void PrintBattleLog()
    {
        Debug.Log("전투 로그");
        for (int i = 0; i < battleLog.Length; i++)
        {
            int index = (logIndex + i) % battleLog.Length;
            if (!string.IsNullOrEmpty(battleLog[index]))
            {
                Debug.Log(battleLog[index]);
            }
        }
    }
}
