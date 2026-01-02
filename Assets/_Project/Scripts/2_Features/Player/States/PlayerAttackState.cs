using UnityEngine;

namespace ProjectGauss.Player
{
    public class PlayerAttackState : PlayerStateBase
    {
        public PlayerAttackState(PlayerController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Debug.Log("공격모드 Enter");
        }

        public override void Execute()
        {
            Vector2 input = controller.Input.Player.Move.ReadValue<Vector2>();
            if (input.sqrMagnitude > .01f)
            {
                controller.StateMachine.ChangeState(controller.MoveState);
                return;
            }

            Transform target = controller.Targeting.CurrentTarget;
            if (target == null)
            {
                controller.StateMachine.ChangeState(controller.IdleState);
                return;
            }

            controller.LookAt(target.position);

            // TODO: 공격 로직 구현
        }
    }
}
