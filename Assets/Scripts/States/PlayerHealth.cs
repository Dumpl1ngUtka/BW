using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    private PlayerParameters _parameters;
    private PlayerStats _playerStats;
    private Player _player;
    private float _healthMax;
    private float _health;

    public Action<float> OnHealthChange;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _player = GetComponent<Player>();
        _parameters = _playerStats.PlayerParameters;
        UpdateMaxHealth();
        _health = _healthMax;
        _playerStats.ParametersChanged += UpdateMaxHealth;
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

    private void UpdateMaxHealth()
    {
        _healthMax = _parameters.Health.Value;
    }
}
