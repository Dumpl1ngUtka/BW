using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private PlayerStamina _playerStamina;
    [SerializeField] private Image _barImage;
    private Shader _barShader;

    private void Awake()
    {
        _barShader = _barImage.material.shader;
        _playerStamina.OnStaminaChange += UpdateBar;
    }
    private void UpdateBar(float value)
    {
        _barImage.fillAmount = value;
    }
}
