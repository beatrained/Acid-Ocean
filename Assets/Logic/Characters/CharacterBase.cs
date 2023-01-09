using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public bool CanMove { get; set; }
    public Stats Stats { get; set; }

    public virtual void SetStats(Stats stats)
    {
        Stats = stats;
    }

    public virtual void ApplyDamage(float damage) { }
}
