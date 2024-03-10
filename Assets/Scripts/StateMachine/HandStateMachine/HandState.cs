using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class HandState : State
    {
        protected HandStateMachine StateMachine { get; private set; }
        protected MovementStateMachine MoveStateMachine { get; private set; }

        public HandState(HandStateMachine stateMachine) : base(stateMachine)
        {
            StateMachine = stateMachine;
            MoveStateMachine = stateMachine.MovementStateMachine;
        }
    }
}