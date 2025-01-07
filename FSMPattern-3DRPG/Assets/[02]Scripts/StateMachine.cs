using System;
using System.Collections.Generic;
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