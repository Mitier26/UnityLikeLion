public interface IState
{
    StateMachine Fsm { get; set; }

    void InitState(IBlackboardBase blackboard);
    
    void Enter();
    void UpdateState(float deltaTime);
    void Exit();
}