using System.Collections;
using UnityEngine;
using static Noises.PlayerNoise;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Vector2 _movementDirection;
    private Vector2 _playerInputDirection;
    private int _availableJumpCount;
    private bool _hasCoyotJump;
    private bool _isCanDodge;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private PlayerInputSystem _inputSystem;
    private Player _player;
    private PlayerStats _playerStats;
    private Condition _currentCondition => _player.CurrentCondition;
    private PlayerParameters _stats => _playerStats.PlayerParameters;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _player = GetComponent<Player>();
        _playerStats = GetComponent<PlayerStats>();
        SetDefaultCondition();
        _inputSystem = _player.InputSystem;
        NoiseValue = 0;//test
    }

    private void Update()
    {
        _playerInputDirection = _inputSystem.Movement.Move.ReadValue<Vector2>();
        DodgeInput();
        JumpInput();
        BlockInput();
        AttackInput();
    }

    private void BlockInput()
    {
        if (_inputSystem.Movement.Block.IsPressed())
        {
            _player.ChangeCurrentCondition(Player.ConditionType.Block);
            _animator.SetBool("isBlock", true);
        }
        else
        {
            _animator.SetBool("isBlock", false);
        }
    }    
    
    private void AttackInput()
    {
        if (_inputSystem.Movement.Attack.triggered)
            Attack();
    }

    private void JumpInput()
    {
        var isCanJump = _hasCoyotJump || _isGrounded || _availableJumpCount > 0;
        if (_inputSystem.Movement.Jump.triggered && isCanJump)
            Jump();
    }    
    
    private void DodgeInput()
    {
        if (_inputSystem.Movement.Dodge.triggered && _isCanDodge)
            StartCoroutine(Dodge());
    }

    private void FixedUpdate()
    {
        if (_currentCondition.IsCanControlCharacter)
            HorizontalMove(); 
        CheckCollision();
        Gravity();
        ApplyMovement();
    }

    private IEnumerator Dodge()
    {
        _isCanDodge = false;
        _player.ChangeCurrentCondition(Player.ConditionType.Dodge);
        _movementDirection.x = 0;
        if (_playerInputDirection.x != 0)
            _movementDirection.x = _stats.DodgeSpeed.Value * (_playerInputDirection.x > 0 ? 1 : -1);
        yield return new WaitForSeconds(_stats.DodgeTime.Value);
        SetDefaultCondition();
    }    
    
    private void Attack()
    {
        _player.ChangeCurrentCondition(Player.ConditionType.Attack);
        _animator.SetTrigger("Attack");
    }

    private void SetDefaultCondition()
    {
        _player.ChangeCurrentCondition(Player.ConditionType.Move);
    }

    private void HorizontalMove()
    {
        if (_playerInputDirection.x == 0)
        {
            var deceleration = _isGrounded ? _stats.GroundDeceleration.Value : _stats.AirDeceleration.Value;    
            _movementDirection.x = Mathf.MoveTowards(_movementDirection.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            var maxSpeed = _currentCondition == _player.MoveCondition? _stats.MaxSpeed.Value : _stats.MaxSpeed.Value * _stats.BlockMaxSpeedModifier.Value;
            _movementDirection.x = Mathf.MoveTowards(_movementDirection.x, _playerInputDirection.x * maxSpeed, _stats.Acceleration.Value * Time.fixedDeltaTime);
        }
    }

    private void CheckCollision()
    {
        Physics2D.queriesStartInColliders = false;

        bool groundHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, _stats.GrounderDistance, ~_player.GroundLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.up, _stats.GrounderDistance, ~_player.GroundLayer);
        if (ceilingHit)
            _movementDirection.y = Mathf.Min(0, _movementDirection.y);

        if (!_isGrounded && groundHit)
        {
            _isGrounded = true;
        }
        else if (_isGrounded && !groundHit)
        {
            _isGrounded = false;
            StartCoroutine(CoyoteTime());
        }

        if (!_isCanDodge && _isGrounded)
            _isCanDodge = true;
        if (_isGrounded && _availableJumpCount < 1)
            _availableJumpCount = 1;

        Physics2D.queriesStartInColliders = true;
    }

    private IEnumerator CoyoteTime()
    {
        _hasCoyotJump = true;
        yield return new WaitForSeconds(_stats.CoyoteTime);
        _hasCoyotJump = false;
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        _movementDirection.y = _stats.JumpPower;

        if (!_hasCoyotJump)
            _availableJumpCount--;
    }

    private void Gravity()
    {
        if (!_currentCondition.IsUsedGravity || _isGrounded && _movementDirection.y < 0)
        {
            _movementDirection.y = 0;
            return;
        }

        var inAirGravity = _stats.FallAcceleration;
        var maxFallSpeed = _playerInputDirection.y < 0 ?
            _stats.MaxFallSpeed * _stats.FastFallMaxSpeedModifier : _stats.MaxFallSpeed;
        _movementDirection.y = Mathf.MoveTowards(_movementDirection.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = _movementDirection;
        if (_movementDirection.x != 0)
        {
            _animator.SetBool("IsRun", true);
            
        }
        else
            _animator.SetBool("IsRun", false);
    }
}
