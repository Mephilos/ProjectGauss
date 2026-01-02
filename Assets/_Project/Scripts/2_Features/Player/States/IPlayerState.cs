namespace ProjectGauss.Player
{
    public interface IPlayerState
    {
        void Enter();
        void Execute();
        void PhysicsExecute();
        void Exit();
    }
}