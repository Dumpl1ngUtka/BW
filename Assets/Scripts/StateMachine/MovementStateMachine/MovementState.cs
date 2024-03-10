using UnityEngine;

namespace StateMachine
{
    public abstract class MovementState : State
    {
        private Vector2 _movementDirection;
        private CapsuleCollider2D _collider;

        protected MovementStateMachine StateMachine { get; private set; }
        protected HandStateMachine HandStateMachine { get; private set; }
        protected bool IsGround { get; private set; }

        public MovementState(MovementStateMachine stateMachine) : base(stateMachine)
        {
            StateMachine = stateMachine;
            _collider = stateMachine.Collider;
            HandStateMachine = stateMachine.HandStateMachine;
        }

        public override void OwnFixedUpdate()
        {
            CheckCollision();
        }

        private void CheckCollision()
        {
            Physics2D.queriesStartInColliders = false;
            bool groundHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, Stats.GrounderDistance);
            bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.up, Stats.GrounderDistance);
            if (ceilingHit)
                _movementDirection.y = Mathf.Min(0, _movementDirection.y);

            if (!IsGround && groundHit)
            {
                IsGround = true;
            }
            else if (IsGround && !groundHit)
            {
                IsGround = false;
            }

            Physics2D.queriesStartInColliders = true;
        }
    }
}