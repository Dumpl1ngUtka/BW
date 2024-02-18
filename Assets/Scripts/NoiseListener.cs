using System.Collections.Generic;
using UnityEngine;

namespace Noises
{
    public class NoiseListener : MonoBehaviour
    {
        public List<Noise> NoiseSource { get; private set; } = new List<Noise>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Noise noise))
                NoiseSource.Add(noise);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Noise noise))
                NoiseSource.Remove(noise);
        }
    }
}
