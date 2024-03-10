using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Attack : HandState
    {
        private PlayerInputSystem _inputActions;
        public Attack(HandStateMachine stateMachine) : base(stateMachine)
        {
            _inputActions = stateMachine.InputActions;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            var direction = AttackDirection();
        }

        public override void OwnUpdate()
        {
            base.OwnUpdate();
            StateMachine.ChangeState(StateMachine.IdleState);
        }

        private Vector2 AttackDirection()
        {
            var inputDirection = _inputActions.Movement.Move.ReadValue<Vector2>();
            if (inputDirection == Vector2.zero)
                return Vector2.up;
            else if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
                return inputDirection.x > 0? Vector2.right : Vector2.left;
            else
                return inputDirection.y > 0 ? Vector2.up : Vector2.down;
        }
    }
}
