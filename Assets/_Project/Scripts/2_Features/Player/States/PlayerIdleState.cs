using UnityEngine;

namespace ProjectGauss.Player
{
    public class PlayerIdleState : PlayerStateBase
    {
        public PlayerIdleState(PlayerController controller) : base(controller)
        { }

        public override void Execute()
        {
            Vector2 input = controller.Input.Player.Move.ReadValue<Vector2>();
            if (input.sqrMagnitude > 0.01f)
            {
                controller.StateMachine.ChangeState(controller.MoveState);
                return;
            }

            if (controller.Targeting.CurrentTarget != null)
            {
                controller.StateMachine.ChangeState(controller.AttackState);
                return;
            }

            controller.LookAtMouse();
        }
    }
}