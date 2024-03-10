using System.Collections;
using UnityEngine;

namespace Items.Health
{
    public class HealthPotion : Item
    {
        private PlayerHealth _playerHealth;
        private int _healthPrecent = 40;
        private float _healthTime = 0;

        public HealthPotion(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void Use()
        {
            if (_healthTime == 0)
                _playerHealth.AddHealthByPrecent(_healthPrecent);
            else
                Health(_healthPrecent, _healthTime);

        }

        private IEnumerator Health(int procent, float time)
        {
            var procentPerSecond = procent / time;
            var waitForEndOfFrame = new WaitForEndOfFrame();
            var timer = 0f;
            while (timer < time)
            {
                _playerHealth.AddHealthByPrecent(procentPerSecond * Time.deltaTime);
                timer += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
        }

        public bool TryToFill(int value)
        {
            var newCount = CurrentCount + value;
            if (newCount > MaxCount)
                return false;
            Fill(newCount);
            return true;
        }

        private void Fill(int value)
        {
            CurrentCount += value; 
        }

        public void AddUpgrade(HealthPotionUpgrade upgrade)
        {

        }
    }
}