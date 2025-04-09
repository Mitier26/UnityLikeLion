using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { None, Idle, Patrol, Trace, Attack, Hit, Dead }


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    private EnemyState _currentState;
     public EnemyState CurrentState => _currentState;
     
     public void SetState(EnemyState newState)
     {
         
     }
}
