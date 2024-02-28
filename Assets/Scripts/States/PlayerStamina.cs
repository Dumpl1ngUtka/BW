using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private PlayerParameters _parameters;
    private Player _player;
    private float _healthMax;
    private float _health;
    private float _timeWithoutDamage;

    public Action<float> OnStaminaChange;
}
