using UnityEngine;

namespace StateMachine
{
    public class Jump : MovementState, IMovable, ICanUseHand
    {
        private Rigidbody2D _rigidbody;
        private PlayerInputSystem _inputActions;
        private float _verticalMove;
        private float _maxSpeed => Stats.MaxSpeed.Value;
        private float _acceleration => Stats.Acceleration.Value;
        private float _deceleration => Stats.AirDeceleration.Value;
        private float _inAirGravity => Stats.FallAcceleration;


        public Jump(MovementStateMachine stateMachine) : base(stateMachine)
        {
            _rigidbody = stateMachine.Rigidbody;
            _inputActions = stateMachine.InputActions;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Stats.JumpPower);
            _verticalMove = Stats.JumpPower;
        }

        public override void OwnUpdate()
        {
            base.OwnUpdate();
            if (_inputActions.Movement.Jump.WasPerformedThisFrame() && StateMachine.AvailableJumpCount > 0)
            {
                StateMachine.ChangeState(StateMachine.JumpState);
                StateMachine.AvailableJumpCount--;
            }
            if (_inputActions.Movement.Dodge.IsPressed() && StateMachine.AvailableDodgeCount > 0)
            {
                StateMachine.ChangeState(StateMachine.DodgeState);
                StateMachine.AvailableDodgeCount--;
            }
        }

        public override void OwnFixedUpdate()
        {
            base.OwnFixedUpdate();
            if (IsGround)
                StateMachine.ChangeState(StateMachine.MoveState);
            else if (_rigidbody.velocity.y <= 0)
                StateMachine.ChangeState(StateMachine.FallState);
        }

        public float VerticalMove()
        {
            if (_inputActions.Movement.Move.ReadValue<Vector2>().y < 0)
                _verticalMove = 0;
            else
                _verticalMove = Mathf.MoveTowards(_verticalMove, 0, _inAirGravity * Time.fixedDeltaTime);
            return _verticalMove;
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
    }
}