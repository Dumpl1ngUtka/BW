using UnityEngine;


namespace StateMachine
{
    public class Fall : MovementState, IMovable, IGravitate, ICanUseHand
    {
        private Rigidbody2D _rigidbody;
        private PlayerInputSystem _inputActions;

        private float _maxSpeed => Stats.MaxSpeed.Value;
        private float _acceleration => Stats.Acceleration.Value;
        private float _deceleration => Stats.AirDeceleration.Value;

        private float _fallAcceleration => Stats.FallAcceleration;

        public Fall(MovementStateMachine stateMachine) : base(stateMachine)
        {
            _inputActions = stateMachine.InputActions;
            _rigidbody = stateMachine.Rigidbody;
        }
        public override void OnEnter()
        {
            base.OnEnter();
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
        }

        public float Gravity()
        {
            var pressedDown = _inputActions.Movement.Move.ReadValue<Vector2>().y < 0;
            var maxFallSpeed = Stats.MaxFallSpeed * (pressedDown ? Stats.FastFallMaxSpeedModifier : 1);
            return Mathf.MoveTowards(_rigidbody.velocity.y, -maxFallSpeed, _fallAcceleration * Time.fixedDeltaTime);
        }

        public float VerticalMove()
        {
            return 0;
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