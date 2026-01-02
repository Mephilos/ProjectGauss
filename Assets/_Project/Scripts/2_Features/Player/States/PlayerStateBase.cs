namespace ProjectGauss.Player
{
    public abstract class PlayerStateBase : IPlayerState
    {
        protected PlayerController controller;

        public PlayerStateBase(PlayerController controller)
        {
            this.controller = controller;
        }

        public virtual void Enter() { }
        public virtual void Execute() { }
        public virtual void PhysicsExecute() { }
        public virtual void Exit() { }
    }
}

