using UnityEngine;

public class PlayerParameter
{
    private readonly float _minLevel = 0;
    private readonly float _maxLevel = 10;
    private float _level;
    private float _minValue;
    private float _maxValue;

    public float Level
    {
        get { return _level; }
        set
        {
            var newLevel = value + _level;
            if (newLevel >= _minLevel || newLevel <= _maxLevel)
                _level = newLevel;
        }
    }

    public float Value => Mathf.Lerp(_minValue, _maxValue, _level / 10);

    public PlayerParameter(float minValue, float maxValue, float level = 0)
    {
        _minValue = minValue;
        _maxValue = maxValue;
        Level = level;
    }
}