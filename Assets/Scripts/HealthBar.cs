using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image _barImage;

    private void Awake()
    {
       // _playerStamina.OnHealthChange += UpdateBar;
    }
    private void UpdateBar(float value)
    {
        _barImage.fillAmount = value;
    }
}
