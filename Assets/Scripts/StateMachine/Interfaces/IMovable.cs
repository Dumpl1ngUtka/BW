using UnityEngine;

namespace StateMachine
{
    public interface IMovable
    {
        public float HorizontalMove();
        public float VerticalMove();
    }

}