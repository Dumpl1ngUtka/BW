using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Dodge : MovementState, IMovable
    {
        private PlayerInputSystem _inputActions;
        private float _horizontalMove;
        private float _timer;
        public Dodge(MovementStateMachine stateMachine) : base(stateMachine)
        {
            _inputActions = stateMachine.InputActions;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _timer = 0;
            var directionX = _inputActions.Movement.Move.ReadValue<Vector2>().x;
            if (directionX > 0.3f)
                _horizontalMove = Stats.DodgeSpeed.Value;
            else if (directionX < -0.3f)
                _horizontalMove = -Stats.DodgeSpeed.Value;
            else
                _horizontalMove = 0;
        }

        public override void OwnUpdate()
        {
            base.OwnUpdate();
            if (_timer >= Stats.DodgeTime.Value)
            {
                State nextState = IsGround ? StateMachine.MoveState : StateMachine.FallState;
                StateMachine.ChangeState(nextState);
            }
            _timer += Time.deltaTime;
        }

        public float HorizontalMove()
        {
            return _horizontalMove;
        }

        public float VerticalMove()
        {
            return 0;
        }
    }
}