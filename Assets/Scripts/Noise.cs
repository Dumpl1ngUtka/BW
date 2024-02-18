using UnityEngine;

namespace Noises
{
    public class Noise : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _minNoise;
        [SerializeField, Range(0, 1)] private float _maxNoise;
        private float _noiseValue;
        public float NoiseValue
        {
            get { return _noiseValue; }
            set { _noiseValue = Mathf.Clamp(value, _minNoise, _maxNoise); }
        }
    }
}

