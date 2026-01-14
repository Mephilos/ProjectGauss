using UnityEngine;

namespace ProjectGauss.Player
{
    public class PlayerAttackState : PlayerStateBase
    {
        public PlayerAttackState(PlayerController controller) : base(controller) { }

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
            controller.Aim(target.position);

            controller.CurrentWeapon.Fire(
                controller.FirePoint.position, controller.FirePoint.forward,
                target.position, controller.Systems
            );
        }
    }
}
