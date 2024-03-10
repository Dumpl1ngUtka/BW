using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
    private PlayerStats _playerStats;
    private float _health;
    private float _healthMax;

    public Action<float> OnHealthChange;
    public Action HealthIsZero;

    public void Start()
    {
        UpdateMaxHealth();
        _health = _healthMax;
    }

    private void OnEnable()
    {
        if (_playerStats == null)
            _playerStats = GetComponent<PlayerStats>();

        _playerStats.ParametersChanged += UpdateMaxHealth;
    }

    private void OnDisable()
    {
        _playerStats.ParametersChanged -= UpdateMaxHealth;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHitBox>(out var hitBox))
            TakeDamage(hitBox.Damage);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChange?.Invoke(_health / _healthMax);
        if (_health < 0)
            HealthIsZero?.Invoke();
    }


    private void UpdateMaxHealth()
    {
        _healthMax = _playerStats.PlayerParameters.Health.Value;
        OnHealthChange?.Invoke(_health / _healthMax);
    }

    public void AddHealthByPrecent(float precent)
    {
        if (precent <= 0)
            return;

        _health += _healthMax * precent / 100;
        if (_health > 0)
            _health = 0;

        OnHealthChange?.Invoke(_health / _healthMax);
    }
}
