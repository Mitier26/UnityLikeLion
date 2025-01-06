using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using STC = StateTypesClasses;

public enum StaterType
{
    None,
    Player,
    Monster,
    Max
}

// 확장 메서드 + 팩토리 메서드 패턴 + 컬렉션 입니다.
public static class StateFactory
{
    public static List<IState> CreateStates(this StateMachine stateMachine, StaterType staterType)
    {
        List<IState> states = new List<IState>();
        
        switch (staterType)
        {
            case StaterType.Player:
            {
                states.Add(stateMachine.AddComponent<IdleState>());
                states.Add(stateMachine.AddComponent<WalkState>());
                states.Add(stateMachine.AddComponent<JumpState>());
            }
                break;
            case StaterType.Monster:
                {
                    states.Add(stateMachine.AddComponent<IdleState_Monster>());
                    states.Add(stateMachine.AddComponent<WalkState_Monster>());
                    states.Add(stateMachine.AddComponent<SkillState_Monster>());
                }
                break;
        }

        return states;
    }
}

public class StateMachine : MonoBehaviour
{
    [SerializeField] private string defaultState;
    
    private IState currentState;
    private Dictionary<STC.StateTypes, IState> states = new Dictionary<STC.StateTypes, IState>();

    public void Run(StaterType staterType)
    {
        IBlackboardBase blackboardDefault = GetComponent<IBlackboardBase>();
        blackboardDefault.InitBlackboard();
        List<IState> states = this.CreateStates(staterType);
        foreach (var state in states)
        {
            AddState(state, blackboardDefault);
        }
        
        ChangeState(Type.GetType(defaultState));
    }
    
    public void AddState(IState state, IBlackboardBase blackboard){
        state.Fsm = this;
        state.InitState(blackboard);
        states.Add(STC.GetState(state.GetType()), state);
    }
    
    public void ChangeState<T>() where T : IState
    {
        ChangeState(typeof(T));
    }
    
    public void ChangeState(Type stateType)
    {
        ChangeState(STC.GetState(stateType));
    }

    public void ChangeState(STC.StateTypes stateType)
    {
        currentState?.Exit();

        if (!states.TryGetValue(stateType, out currentState)) return;
        
        currentState?.Enter();
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.UpdateState(Time.deltaTime);
    }
}