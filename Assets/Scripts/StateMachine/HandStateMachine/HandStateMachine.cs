using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class HandStateMachine : PlayerStateMachine
    {
        public MovementStateMachine MovementStateMachine { get; private set; }
        public Attack AttackState { get; private set; }
        public Block BlockState { get; private set; }
        public Idle IdleState { get; private set; }

        protected override void Start()
        {
            base.Start();
            MovementStateMachine = GetComponent<MovementStateMachine>();
            SetStates();
            ChangeState(IdleState);
        }

        protected override void SetStates()
        {
            AttackState = new Attack(this);
            BlockState = new Block(this);
            IdleState = new Idle(this);
        }
    }
}