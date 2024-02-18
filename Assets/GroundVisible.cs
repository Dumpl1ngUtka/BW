using UnityEngine;
using Noises;

public class GroundVisible : MonoBehaviour
{
    [SerializeField] private Material _groundBlocks;
    [SerializeField] private Transform _player;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _groundBlocks.SetFloat("_SideRatio", (float)Screen.width / Screen.height);
    }

    private void FixedUpdate()
    {
        var screenPos = _mainCamera.WorldToScreenPoint(_player.transform.position);
        screenPos.y /= Screen.height;
        screenPos.x /= Screen.width;
        var radius = (1 - PlayerNoise.NoiseValue);
        _groundBlocks.SetVector("_PlayerPos", screenPos);
        _groundBlocks.SetFloat("_Radius", radius);
    }
}
