using AcidOcean.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatsManagerPlayer : CharacterStatsManagerBase
{
    private ScrPlayerCharacter _playerExtendedStats;
    private float _speedOnFourLegs;
    public float SpeedOnFourLegs => _speedOnFourLegs;

    private float _lastHitTakenDamageAmount;

    private void Awake()
    {
        RunOnAwake();
        GlobalEventManager.PlayerRecieveDamage += GlobalEventManager_PlayerRecieveDamage;
        _playerExtendedStats = (ScrPlayerCharacter)CharScriptable;
        _speedOnFourLegs = _playerExtendedStats.SpeedOnFourLegs;
    }

    private void GlobalEventManager_PlayerRecieveDamage(float incomingDamage)
    {
        _lastHitTakenDamageAmount = incomingDamage;
        TakeDamage(_lastHitTakenDamageAmount);
    }
}
