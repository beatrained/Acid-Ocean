using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public bool CanMove { get; set; }
    public BasicActorStats CharStats { get; set; }

    /// <summary>
    ///     грузит Character stats.
    /// </summary>
    public virtual void SetStats(BasicActorStats stats)
    {
        CharStats = stats;
    }

    public virtual void ApplyDamage(float damage) { }
}
