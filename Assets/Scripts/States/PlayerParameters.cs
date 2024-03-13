using System;
using UnityEngine;

public class PlayerParameters 
{
    public PlayerParameter Health { get; private set; } = new (100,20);
    public PlayerParameter Stamina { get; private set; } = new(100,10);
    public PlayerParameter MaxSpeed { get; private set; } = new(7, 1);
    public PlayerParameter Acceleration { get; private set; } = new(60, 2);
    public PlayerParameter GroundDeceleration { get; private set; } = new(30, 1);
    public PlayerParameter AirDeceleration { get; private set; } = new(15, 1);
    public float GrounderDistance { get; private set; } = 0.1f;//Range(0f, 0.5f)
    public PlayerParameter Damage { get; private set; } = new(10, 1);
    public PlayerParameter SoundRange { get; private set; } = new (80, 5);
    public PlayerParameter AdditionalNoise { get; private set; } = new(20, -1);

    #region Dodge

    public PlayerParameter DodgeTime { get; private set; } = new(0.2f,0.01f);
    public PlayerParameter DodgeSpeed { get; private set; } = new(20,1);
    public PlayerParameter DodgeIdleTime { get; private set; } = new(0.05f, -0.01f);

    #endregion

    public PlayerParameter BlockMaxSpeedModifier { get; private set; } = new(0.3f,0.05f);
    public int AirJumpCount { get; private set; } = 1;
    public float JumpPower { get; private set; } = 24;
    public float MaxFallSpeed { get; private set; } = 40;
    public float FallAcceleration { get; private set; } = 60;
    public float CoyoteTime { get; private set; } = 0.15f;
    public float JumpBuffer { get; private set; } = 0.2f;
    public float FastFallMaxSpeedModifier { get; private set; } = 2;
    public int MaxBonusCount { get; private set; } = 5;
}
