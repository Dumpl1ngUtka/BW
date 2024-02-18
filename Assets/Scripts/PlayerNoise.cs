using UnityEngine;

namespace Noises
{
    public static class PlayerNoise
    {
        [SerializeField, Range(0, 1)] private static float _minNoise = 0;
        [SerializeField, Range(0, 1)] private static float _maxNoise = 1;
        [SerializeField] private static float _speed = 2;
        private static float _noiseValue;
        private static float _currentNoiseValue;
        public static float NoiseValue
        {
            get { return _currentNoiseValue = Mathf.MoveTowards(_currentNoiseValue, _noiseValue, Time.deltaTime * _speed); }
            set { _noiseValue = Mathf.Clamp(value, _minNoise, _maxNoise); }

        }
    }
}

