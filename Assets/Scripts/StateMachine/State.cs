using UnityEngine;

namespace StateMachine
{
    public abstract class State
    {
        private PlayerStats _playerStats;
        protected PlayerParameters Stats => _playerStats.PlayerParameters;

        public State(PlayerStateMachine stateMachine)
        {
            _playerStats = stateMachine.PlayerStats;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OwnUpdate()
        {
        }

        public virtual void OwnFixedUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
   
