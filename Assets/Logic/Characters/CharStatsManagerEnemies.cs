using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatsManagerEnemies : CharacterStatsManagerBase
{
    private ScrEnemyCharacterMelee _enemyExtendedStats;
    private float _agroRange;
    public float AggroRange => _agroRange;

    private void Awake()
    {
        RunOnAwake();
        _enemyExtendedStats = (ScrEnemyCharacterMelee)CharScriptable;
        _agroRange = _enemyExtendedStats.AgroRange;
    }
}
