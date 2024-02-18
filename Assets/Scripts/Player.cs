using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerParameters Parameters;
    public LayerMask GroundLayer;
    public Condition MoveCondition;
    public Condition DodgeCondition;
    public Condition BlockCondition;
    public PlayerInputSystem InputSystem { get; private set; }
    public Condition CurrentCondition { get; private set; }
    public enum ConditionType
    {
        Move,
        Dodge,
        Block,
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
        }
    }
}
