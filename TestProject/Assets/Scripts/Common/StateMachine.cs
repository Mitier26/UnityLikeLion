public class StateMachine 
{
    IState _currentState;

    public void ChangeState(IState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.UpdateState();
    }
}
// MonoBehaviour 가 없는 이유 : MonoBehaviour 가 있는 것은 유니티에서 Object 형태로 있어야 실행된다.
// 오브젝트 없이 여러 곳에서 사용할 수 있게 하기 위해 상속 받지 않았다.

// StateMachine : state의 변화와 현재 상태 업데이트 에만 사용
// 이것을 캐릭터나 몬스터 같은 것에서 사용 한다.
// 상태를 변경 하고 관리 하는 것을 만들 었다.
// 다음은 각 상태를 만든다.
// 상태는 CharacterController에서 관리한다 
// 각 상태를 CharacterController가 필요하다.