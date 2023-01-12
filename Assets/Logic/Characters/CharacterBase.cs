using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public bool CanMove { get; set; }
    public ScrUnitBase CharStats { get; set; }

    /// <summary>
    ///     грузит Character stats.
    /// </summary>
    public virtual void SetStats(ScrUnitBase stats)
    {
        CharStats = stats;
    }

    public virtual void ApplyDamage(float damage) { }
}
