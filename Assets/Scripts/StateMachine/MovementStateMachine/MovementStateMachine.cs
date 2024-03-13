using UnityEngine;

namespace StateMachine
{
    public class MovementStateMachine : PlayerStateMachine
    {
        private int _availableJumpCount;
        private int _availableDodgeCount;
        private IMovable _movable;
        private IGravitate _gravitate;

        public HandStateMachine HandStateMachine { get; private set; }
        public Move MoveState { get; private set; }
        public Dodge DodgeState { get; private set; }
        public Jump JumpState { get; private set; }
        public Fall FallState { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }

        public bool IsCanUseHand => CurrentState as ICanUseHand != null;
        public int AvailableJumpCount
        {
            get { return _availableJumpCount; }
            set { _availableJumpCount = Mathf.Clamp(value, 0, 1); }
        }

        public int AvailableDodgeCount
        {
            get { return _availableDodgeCount; }
            set { _availableDodgeCount = Mathf.Clamp(value, 0, 1); }
        }

        protected override void Start()
        {
            base.Start();
            HandStateMachine = GetComponent<HandStateMachine>();
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CapsuleCollider2D>();
            SetStates();
            ChangeState(MoveState);
        }

        protected override void SetStates()
        {
            MoveState = new Move(this);
            DodgeState = new Dodge(this);
            JumpState = new Jump(this);
            FallState = new Fall(this);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector2 moveDirection = Vector2.zero;
            if (_movable != null)
            {
                moveDirection.x += _movable.HorizontalMove();
                moveDirection.y += _movable.VerticalMove();
            }
            if (_gravitate != null)
                moveDirection.y += _gravitate.Gravity();
            Rigidbody.velocity = moveDirection;
        }

        public override void ChangeState(State newState)
        {
            base.ChangeState(newState);
            _movable = CurrentState as IMovable;
            _gravitate = CurrentState as IGravitate;
        }
    }
}

