public abstract class BaseState<T> : IState
{
    protected T _controller;

    public BaseState(T controller)
    {
        _controller = controller;
    }
    
    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();
    
    // 다른곳에서 IState만 상속 받아서 만들 수 도 있지만 BaseState<T> 를 만든 이유
    // IState를 상속 받아서 만들 경우 상속 받는 모든 곳에서는 Enter, UpdateState, Exit를 구현해야 한다.
    // _controller 를 초기화는 부분을 반복 하지 않아도 된다.
    
    // BaseState를 만든 이유와 필요 이유
    // 이것이 없다면 Character_IdleState, Character_RenState 등 에서 
    // 누구의 상태를 변경하는지에 대한 것이 들어 가야한다.
    // 상태를 만들 때 마다 같은 코드를 반복 해햐 하는 것이다.
    // DRY 원칙 : 중복 제거 원칙에 의해 반복되는 것을 어떻게 모을 까를 생각해야한다.
    // 지금은 3개의 상태 지만 상태가 10개, 100개 이면? 
    // 단일 책임 원칙 : 각 상태는 상태에 관한 것만 관여하고 초기화는 여기서 한다.
    // 의존성 역전 원칙 : CharacterController가 직접 관리 하는 것이 아니고 BaseState를 통해 관리하게 한다.
    
    // 다음에 만들 것은 무엇인가?
    // 상태 변화를 관리할 것?, 각 상태들?
    // StateMachine
}
