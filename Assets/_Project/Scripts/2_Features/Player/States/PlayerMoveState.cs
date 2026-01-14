using UnityEngine;

namespace ProjectGauss.Player
{
    public class PlayerMoveState : PlayerStateBase
    {
        public PlayerMoveState(PlayerController controller) : base(controller) { }

        public override void Enter()
        {
            Debug.Log("이동 모드 진입");
        }

        public override void PhysicsExecute()
        {
            Vector2 input = controller.Input.Player.Move.ReadValue<Vector2>();
            Vector3 dir = new Vector3(input.x, 0, input.y).normalized;

            if (input.sqrMagnitude > 0.01f)
            {
                controller.Move(dir);
                // controller.LookAtMouse();
            }
            else
            {

                if (controller.Targeting.CurrentTarget != null)
                    controller.StateMachine.ChangeState(controller.AttackState);
                else
                    controller.StateMachine.ChangeState(controller.IdleState);
            }
        }
    }
}