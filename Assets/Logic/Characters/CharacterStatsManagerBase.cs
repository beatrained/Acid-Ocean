using UnityEngine;
using AcidOcean.Game;

public class CharacterStatsManagerBase : MonoBehaviour, IDamageable
{
    CharacterBase _thisCharacter;

    [SerializeField] public ScrUnitBase CharScriptable;

    [HideInInspector] public BasicStats CharBasicStats;
    [HideInInspector] public AIStats AiStats;

    public virtual void LoadStats(ScrUnitBase stats)
    {
        CharBasicStats = CharScriptable.BasicStats;
        AiStats = CharScriptable.AIStats;
    }

    public float IncomingDamage { get; set; }

    // ASK Awake
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
            GlobalEventManager.HealthIsZeroved(this.gameObject);
        }
        print("Health = " + CharBasicStats.Health);
    }
    #endregion IDamageable
}
