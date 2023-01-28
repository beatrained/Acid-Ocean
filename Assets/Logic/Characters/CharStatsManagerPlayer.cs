using AcidOcean.Game;
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
        gameObject.GetComponent<Animator>().SetTrigger("TakingHit");
        _lastHitTakenDamageAmount = incomingDamage;
        TakeDamage(_lastHitTakenDamageAmount);
    }
}
