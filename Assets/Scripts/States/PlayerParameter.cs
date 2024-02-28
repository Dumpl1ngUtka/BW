public class PlayerParameter
{
    private readonly float _minLevel = 0;
    private float _level;
    private float _minValue;
    private float _delta;

    public float Level
    {
        get { return _level; }
        set
        {
            var newLevel = value + _level;
            _level = newLevel >= _minLevel? newLevel : _minLevel;
        }
    }

    public float Value => _minValue + _delta * _level;

    public PlayerParameter(float minValue, float delta, float level = 0)
    {
        _minValue = minValue;
        _delta = delta;
        Level = level;
    }
}