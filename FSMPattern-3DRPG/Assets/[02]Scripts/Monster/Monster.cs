using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Blackboard_Monster))]
public class Monster : Entity
{
    Blackboard_Monster monster;
    protected override StaterType EntityStaterType => StaterType.Monster;

    protected override void Start()
    {
        base.Start();
        monster = GetComponent<Blackboard_Monster>();
    }

    private void OnEnable()
    {
        MonsterManager.Instance.AddMonster(this);
    }

    private void OnDisable()
    {
        if(!MonsterManager.Instance.IsUnityNull())
            MonsterManager.Instance.RemoveMonster(this);

    }

    public void OnDetectPlayer(Player player)
    {
        Debug.Log("1111111111"+player);
        Debug.Log("2222222222"+monster);
        monster.target = player;
    }
}
