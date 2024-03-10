using UnityEngine;

namespace UI.Menu
{
    public class BonusMenu : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        private PlayerBonusHolder _playerBonusHolder;

        private void Awake()
        {
            _playerBonusHolder = _playerStats.BonusHolder;
        }
    }
}
