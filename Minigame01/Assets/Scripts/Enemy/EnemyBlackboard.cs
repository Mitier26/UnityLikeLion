using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyBlackboard
{
    public float health;
    public float maxHealth;
    public float speed;
    public float normalSpeed;
    public float angrySpeed;
    public float fleeSpeed;
    public float atk;
    public float detectRange;
    public Transform player;
    public LayerMask playerLayer;
    public bool isPlayer;
}