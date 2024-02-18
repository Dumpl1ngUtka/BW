using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsMenu : MonoBehaviour
{
    [SerializeField] private PlayerSkillLevels _skillLevels;
    [SerializeField] private SkillsMenuCell Health;
    [SerializeField] private SkillsMenuCell Endurance;
    [SerializeField] private SkillsMenuCell Strength;
    [SerializeField] private SkillsMenuCell Dexterity;
    [SerializeField] private SkillsMenuCell Hearing;
    [SerializeField] private SkillsMenuCell Stealth;
    private SkillsMenuCell[] _skillsMenuCells = new SkillsMenuCell[6];
    private int _currentCellIndex = 0;
    private PlayerInputSystem _playerInputSystem;
    private int _skillPoints = 0;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        _playerInputSystem.UI.ChooseItem.started += Select;
        _playerInputSystem.UI.Accept.started += AddLevels;
    }

    private void AddLevels(InputAction.CallbackContext obj)
    {
        foreach (var cell in _skillsMenuCells)
        {
            _skillLevels.LevelUp(cell.Skill, cell.AdditionalLevel);
            cell.RemoveAdditionalLevels();
        }
        _skillPoints = _skillLevels.LevelPoints;
    }

    private void Select(InputAction.CallbackContext obj)
    {
        var input = obj.ReadValue<Vector2>();
        if (Mathf.Abs(input.y) > 0.7f)
        {
            _currentCellIndex += input.y < 0 ? 1 : -1;
            if (_currentCellIndex >= _skillsMenuCells.Length)
                _currentCellIndex = 0;
            else if (_currentCellIndex < 0)
                _currentCellIndex = _skillsMenuCells.Length - 1;
        }
        else if (Mathf.Abs(input.x) > 0.7f)
        {
            if (input.x > 0 && _skillPoints > 0)
            {
                if (_skillsMenuCells[_currentCellIndex].TryAddLevel(1))
                    _skillPoints--;
            }
            else if (input.x < 0)
            {
                if (_skillsMenuCells[_currentCellIndex].TryRemoveLevel(1))
                    _skillPoints++;
            }
        }
        RenderCells();
    }

    private void OnEnable()
    {
        if (_skillsMenuCells[0] == null)
            InitSkillCells();

        _playerInputSystem.Enable();

        _currentCellIndex = 0;
        _skillPoints = _skillLevels.LevelPoints;
        RenderCells();
    }

    private void RenderCells()
    {
        foreach (var cell in _skillsMenuCells)
        {
            cell.Render();
            cell.SetSelect(false);
        }
        _skillsMenuCells[_currentCellIndex].SetSelect(true);
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
        foreach (var cell in _skillsMenuCells)
            cell.RemoveAdditionalLevels();
    }

    private void InitSkillCells()
    {
        Health.Init(_skillLevels.Health);
        Endurance.Init(_skillLevels.Endurance);
        Strength.Init(_skillLevels.Strength);
        Dexterity.Init(_skillLevels.Dexterity);
        Hearing.Init(_skillLevels.Hearing);
        Stealth.Init(_skillLevels.Stealth);

        _skillsMenuCells[0] = Health;
        _skillsMenuCells[1] = Dexterity;
        _skillsMenuCells[2] = Endurance;
        _skillsMenuCells[3] = Hearing;
        _skillsMenuCells[4] = Stealth;
        _skillsMenuCells[5] = Strength;
    }
}
