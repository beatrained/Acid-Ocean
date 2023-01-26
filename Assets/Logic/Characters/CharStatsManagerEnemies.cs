public class CharStatsManagerEnemies : CharacterStatsManagerBase
{
    private ScrEnemyCharacter _enemyExtendedStats;
    private float _agroRange;
    public float AggroRange => _agroRange;

    private void Awake()
    {
        RunOnAwake();
        _enemyExtendedStats = (ScrEnemyCharacter)CharScriptable;
        _agroRange = _enemyExtendedStats.AgroRange;
    }
}
