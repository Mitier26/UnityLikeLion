using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[Serializable]
public class TransitionCodition
{
    public string fromStata;
    public string toStata;
    public string conditionMethod;
}

[Serializable]
public class FSMCofiguration
{
    public string initialState;
    public List<string> states;
    public List<TransitionCodition> transitions;
}


public abstract class StateBase
{
    protected FSMSystem fsm;
    protected MonsterController monster;

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
    
    public void Initialize(FSMSystem _fsm, MonsterController _monster)
    {
        fsm = _fsm;
        monster = _monster;
    }
}

public class IdleState_New : StateBase
{
    public override void OnUpdate()
    {
        Debug.Log("IdleState_New");
    }
}

public class ChaseState_New : StateBase
{
    public override void OnUpdate()
    {
        monster.MoveToTarget();
    }
}

public class AttackState_New : StateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        monster.AttackToTarget();
    }
}

public class FSMSystem : MonoBehaviour
{
    private Dictionary<string, StateBase> states = new();
    private StateBase currentState;
    private FSMCofiguration cofiguration;
    private MonsterController monster;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
            CheckTransitions();
        }
    }

    public void Initialize(string configName, MonsterController monster)
    {
        TextAsset loadedText = Resources.Load<TextAsset>(configName);
        FSMCofiguration configuration = JsonUtility.FromJson<FSMCofiguration>(loadedText.text);

        foreach (string stateName in configuration.states)
        {
            Type stateType = Type.GetType(stateName);
            StateBase state = Activator.CreateInstance(stateType) as StateBase;
            state.Initialize(this, monster);
            states[stateName] = state;
            this.monster = monster;
        }
        
        ChangeState(configuration.initialState);
    }

    private void CheckTransitions()
    {
        foreach (var transition in cofiguration.transitions)
        {
            if (transition.fromStata == currentState.GetType().Name)
            {
                if (CheckCondition(transition.conditionMethod))
                {
                    ChangeState(transition.toStata);
                    break;
                }
            }
        }
    }

    private bool CheckCondition(string methodName)
    {
        MethodInfo methodInfo = monster.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        return methodInfo != null && (bool)methodInfo.Invoke(monster, null);
    }

    private void ChangeState(string newStateName)
    {
        if (currentState != null)
        {
            currentState.OnExit();   
        }

        if (states.TryGetValue(newStateName, out StateBase state))
        {
            currentState = state;
            currentState.OnEnter();
        }
    }

    public string GetCurrentStateName()
    {
        return currentState?.GetType().Name ?? string.Empty;
    }
}

[RequireComponent(typeof(FSMSystem))]
public class MonsterController : MonoBehaviour
{
    public string fsmType = "";
    private FSMSystem fsmSystem;

    [SerializeField]private float detectionRange = 10f;
    [SerializeField]private float attackRange = 2f;
    [SerializeField]private float maxHp = 100f;
    [SerializeField]private float chaseSpeed = 5f;
    
    Transform target;
    
    private float currentHp;
    private bool bAttacked = false;
    
    void Start()
    {
        currentHp = maxHp;
        fsmSystem = GetComponent<FSMSystem>();
        fsmSystem.Initialize(fsmType, this);
    }

    public bool IsDetectedTarget()
    {
        return target != null && (transform.position - target.position).sqrMagnitude <= detectionRange * detectionRange;
    }
    
    public bool IsAttacking()
    {
        return bAttacked;
    }
    
    public void MoveToTarget()
    {
        Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);   
    }
    
    public void AttackToTarget()
    {
        Debug.Log("AttackToTarget");

        StartCoroutine(FinishAttack());
    }

    IEnumerator FinishAttack()
    {
        bAttacked = true;

        yield return new WaitForSeconds(1f);

        bAttacked = false;
    }
}