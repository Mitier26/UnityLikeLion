public interface IState
{
    public void Enter();
    public void UpdateState();
    public void Exit();
    
    // 캐릭터의 이동과 몬스터의 이동등 
    // 처음에는 enum 와 Switch를 이용해 상태를 만들 었겠지만
    // enum을 이용하는 방법은 확장성이 부족하고 상태관리가 어러워
    // 상태 패턴을 만들기 위한 준비이다.
    // 각 상태에 접근 했을 때 , 나 갔을 때 , 실행 중 일 때.
    // 여기서 실행 중일 때는 Update 문 안에 없는데 어떻게 연속작동 할 까?
}
