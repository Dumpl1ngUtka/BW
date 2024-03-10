using Items.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class PlayerItemsHolder : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private HealthPotion _healthPotion;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();  
            _healthPotion = new HealthPotion(_playerHealth);
        }
    }
}
