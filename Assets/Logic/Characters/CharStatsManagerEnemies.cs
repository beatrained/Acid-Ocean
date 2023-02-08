public class CharStatsManagerEnemies : CharacterStatsManagerBase
{
    private ScrEnemyCharacter _enemyExtendedStats;
    private float _agroRange;
    private EnemyType _enemyType;
    public float AgroRange => _agroRange;
    public EnemyType ThisEnemyType => _enemyType;

    private void Awake()
    {
        RunOnAwake();
        _enemyExtendedStats = (ScrEnemyCharacter)CharScriptable;
        _agroRange = _enemyExtendedStats.AgroRange;
        _enemyType = _enemyExtendedStats.EnemyType;
    }
}
