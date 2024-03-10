using UnityEngine;

namespace StateMachine
{
    public abstract class PlayerStateMachine : MonoBehaviour
    {
        protected State CurrentState { get; private set; }
        public PlayerStats PlayerStats { get; private set; }
        public PlayerInputSystem InputActions { get; private set; }

        protected virtual void Awake()
        {
            PlayerStats = GetComponent<PlayerStats>();
            InputActions = new PlayerInputSystem();
        }

        protected virtual void OnEnable()
        {
            InputActions.Enable();
        }

        protected virtual void OnDisable()
        {
            InputActions.Disable();
        }

        protected abstract void SetStates();

        protected virtual void Update()
        {
            CurrentState.OwnUpdate();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState.OwnFixedUpdate();
        }

        public virtual void ChangeState(State newState)
        {
            CurrentState?.OnExit();
            newState.OnEnter();
            CurrentState = newState;
        }
    }
}