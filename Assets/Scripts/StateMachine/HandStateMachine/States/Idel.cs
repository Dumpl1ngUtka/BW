using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class Idle : HandState
    {
        private PlayerInputSystem _inputActions;
        public Idle(HandStateMachine stateMachine) : base(stateMachine)
        {
            _inputActions = stateMachine.InputActions;  
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void OwnUpdate()
        {
            base.OwnUpdate();
            if (_inputActions.Movement.Attack.IsPressed() && MoveStateMachine.IsCanUseHand)
                StateMachine.ChangeState(StateMachine.AttackState);
            
            if (_inputActions.Movement.Block.IsPressed() && MoveStateMachine.IsCanUseHand)
                StateMachine.ChangeState(StateMachine.BlockState);
        }
    }
}