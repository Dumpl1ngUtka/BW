using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    //[SerializeField] private Inventory _inventory;
    private PlayerParameters _parameters;
    private Player _player;
    private float _healthMax;
    private float _health;
    private float _timeWithoutDamage;

    public Action<float> OnHealthChange;
    public Action<float> OnArmorChange;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _parameters = _player.Parameters;
        _healthMax = _parameters.Health.Value;
        _health = _healthMax;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        OnHealthChange?.Invoke(_health / _healthMax);

        if (_health < 0)
            Debug.Log("game over");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitBox = other.GetComponent<EnemyHitBox>();
        if (hitBox != null && _player.CurrentCondition.IsDamageable)
            TakeDamage(hitBox.Damage);
    }

    private void Update()
    {

    }
}
