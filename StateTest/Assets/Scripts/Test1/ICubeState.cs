

public interface ICubeState
{   
    // 인터페이스를 만든다.
    void EnterState(CubeController cube);
    void UpdateState(CubeController cube);
    void ExitState(CubeController cube);
}