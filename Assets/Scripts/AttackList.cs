using UnityEngine;

public class AttackAnimationList
{
    public enum AnimationDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public Animation LeftAttackAnimation;
    public Animation RightAttackAnimation;
    public Animation UpAttackAnimation;
    public Animation DownAttackAnimation;

    public void SetAnimation(AnimationDirection direction, Animation animation)
    {
        switch (direction)
        {
            case AnimationDirection.Left:
                LeftAttackAnimation = animation;
                break;
            case AnimationDirection.Right:
                RightAttackAnimation = animation;
                break;
            case AnimationDirection.Up:
                UpAttackAnimation = animation;
                break;
            case AnimationDirection.Down:
                DownAttackAnimation = animation;
                break;
            default:
                break;
        }
    }
}
