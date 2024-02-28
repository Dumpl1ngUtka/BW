using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerParameters Parameters { get; private set; }
    public LayerMask GroundLayer;
    public Condition MoveCondition;
    public Condition DodgeCondition;
    public Condition BlockCondition;
    public Condition AttackCondition;
    public PlayerInputSystem InputSystem { get; private set; }
    public Condition CurrentCondition { get; private set; }
    public enum ConditionType
    {
        Move,
        Dodge,
        Block,
        Attack
    }

    private void Awake()
    {
        Parameters = new PlayerParameters();
        InputSystem = new PlayerInputSystem();
        InputSystem.Enable();
    }

    public void ChangeCurrentCondition(ConditionType type)
    {
        switch (type)
        {
            case ConditionType.Move:
                CurrentCondition = MoveCondition;
                break;
            case ConditionType.Dodge:
                CurrentCondition = DodgeCondition;
                break;
            case ConditionType.Block:
                CurrentCondition = BlockCondition;
                break;
            case ConditionType.Attack:
                CurrentCondition = AttackCondition;
                break;
            default:
                Debug.LogError("set condition");
                break;
        }
    }
}
