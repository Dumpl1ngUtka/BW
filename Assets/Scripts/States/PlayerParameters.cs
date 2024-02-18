using UnityEngine;

public class PlayerParameters 
{
    [Header("Health")]
    public PlayerParameter Health = new (100,200);
    public PlayerParameter Stamina = new(100,200);

    [Header("MOVEMENT")]
    public PlayerParameter MaxSpeed = new(7, 10);
    public PlayerParameter Acceleration = new(60, 120);
    public PlayerParameter GroundDeceleration = new(30, 60);
    public PlayerParameter AirDeceleration = new(15, 45);
    public float GrounderDistance = 0.05f;//Range(0f, 0.5f)

    public PlayerParameter Damage = new(10, 20);
    public PlayerParameter SoundRange = new (80, 100);
    public PlayerParameter AdditionalNoise = new(20, 0);

    [Header("JUMP")]
    public float JumpPower = 36;
    public float MaxFallSpeed = 40;
    public float FallAcceleration = 110;
    public float CoyoteTime = 0.15f;
    public float JumpBuffer = 0.2f;
    public float FastFallMaxSpeedModifier = 2;

    [Header("DODGE")]
    public PlayerParameter DodgeTime = new(0.2f,0.3f);
    public PlayerParameter DodgeSpeed = new(20,30);

    [Header("Block")]
    public PlayerParameter BlockMaxSpeedModifier = new(0.3f,0.5f);
}
