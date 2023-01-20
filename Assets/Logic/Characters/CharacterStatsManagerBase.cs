using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AcidOcean.Game;

public class CharacterStatsManagerBase : MonoBehaviour, IDamageable
{
    CharacterBase _thisCharacter; //  //  //

    [SerializeField] public ScrUnitBase CharScriptable;
    [HideInInspector] public BasicStats CharBasicStats;                         // ASK why public?
    [HideInInspector] public AIStats AiStats;

    public virtual void LoadStats(ScrUnitBase stats)
    {
        CharBasicStats = CharScriptable.BasicStats;
        AiStats = CharScriptable.AIStats;
    }

    public float IncomingDamage { get; set; }

    public void RunOnAwake()
    {
        LoadStats(CharScriptable);
        _thisCharacter = GetComponent<CharacterBase>();
    }

    #region IDamageable
    public float Health { get { return CharBasicStats.Health; } set { CharBasicStats.Health = value; } }

    public void TakeDamage(float amount)
    {
        CharBasicStats.Health -= amount;
        if (CharBasicStats.Health <= 0)
        {
            LocalEventManager.HealthIsZeroved(this.gameObject);
        }
        print("Health = " + CharBasicStats.Health);
    }
    #endregion IDamageable
}
