using System.Collections;
using UnityEngine;

namespace StateMachine
{
    public class Move : MovementState, IMovable, IGravitate, ICanUseHand
    {
        private PlayerInputSystem _inputActions;
        private Rigidbody2D _rigidbody;

        private float _maxSpeed => Stats.MaxSpeed.Value;
        private float _acceleration => Stats.Acceleration.Value;
        private float _deceleration => Stats.GroundDeceleration.Value;

        public Move(MovementStateMachine stateMachine) : base(stateMachine)
        {
            _inputActions = stateMachine.InputActions;
            _rigidbody = stateMachine.Rigidbody;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            StateMachine.AvailableJumpCount = 10000;
            StateMachine.AvailableDodgeCount = 10000;
        }

        public override void OwnUpdate()
        {
            base.OwnUpdate();
            if (_inputActions.Movement.Jump.IsPressed())
                StateMachine.ChangeState(StateMachine.JumpState);

            if (_inputActions.Movement.Dodge.IsPressed())
                StateMachine.ChangeState(StateMachine.DodgeState);
        }

        public override void OwnFixedUpdate()
        {
            base.OwnFixedUpdate();
            if (!IsGround)
                StateMachine.ChangeState(StateMachine.FallState);
        }

        

        public float HorizontalMove()
        {
            float newHorizontalValue = _rigidbody.velocity.x;
            var inputDirection = _inputActions.Movement.Move.ReadValue<Vector2>();
            if (inputDirection.x == 0)
            {
                newHorizontalValue =
                    Mathf.MoveTowards(newHorizontalValue, 0, _deceleration * Time.fixedDeltaTime);
            }
            else
            {
                newHorizontalValue =
                    Mathf.MoveTowards(newHorizontalValue, inputDirection.x * _maxSpeed, _acceleration * Time.fixedDeltaTime);
            }

            return newHorizontalValue;
        }

        public float Gravity()
        {
            float gravityValue = _rigidbody.velocity.y;
            var inAirGravity = Stats.FallAcceleration;
            var maxFallSpeed = Stats.MaxFallSpeed;
            gravityValue = Mathf.MoveTowards(gravityValue, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            return gravityValue;
        }

        public float VerticalMove()
        {
            return 0;
        }
    }
}