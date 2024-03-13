using UnityEngine;

namespace StateMachine
{
    public abstract class PlayerStateMachine : MonoBehaviour
    {
        protected State CurrentState { get; private set; }
        public PlayerStats PlayerStats { get; private set; }
        public PlayerInputSystem InputActions { get; private set; }

        protected virtual void Start()
        {
            PlayerStats = GetComponent<PlayerStats>();
        }

        protected virtual void OnEnable()
        {
            if (InputActions == null)
                InputActions = new PlayerInputSystem();

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