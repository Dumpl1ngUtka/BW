using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private PlayerStats _unit;
    [SerializeField] private float _damageMultiplier = 1;

    public float Damage => _unit.PlayerParameters.Damage.Value * _damageMultiplier;
}
