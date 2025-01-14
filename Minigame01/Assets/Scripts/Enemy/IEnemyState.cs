public interface IEnemyState
{
    void Enter();
    void Exit();
    void UpdateState();
    void OnTakeDamage();
}
