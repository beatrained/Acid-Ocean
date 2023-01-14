using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //[SerializeField] public ScrUnitBase ActorScriptableObject;
    public bool CanMove { get; set; }
    public ScrUnitBase CharStats { get; set; }

    /// <summary>
    ///     грузит Character stats из scriptable object.
    /// </summary>
    public virtual void SetStats(ScrUnitBase stats)
    {
        CharStats = stats;
    }

    public float CosVisionConeAngle()
    {
        return Mathf.Cos(CharStats.VisionAngle * Mathf.Deg2Rad);
    }

    public virtual void ApplyDamage(float damage) { }



    //===============================================
    #region AI
    public ActorState AState { get; private set; }

    public void ChangeState(ActorState newState)
    {
        AState = newState;
        switch (newState)
        {
            case ActorState.Sleeping:
                HandleSleeping();
                break;
            case ActorState.Wandering:
                HandleWandering();
                break;
            case ActorState.Chasing:
                HandleChasing();
                break;
            case ActorState.Attacking:
                HandleAttacking();
                break;
        }
    }

    public virtual void HandleAttacking()
    {
        print("Attacking!");
    }

    public virtual void HandleChasing()
    {
        print("Chasing someone!");
    }

    public virtual void HandleWandering()
    {
        print("Wander");
    }

    public virtual void HandleSleeping()
    {
        print("Sleep");
    }

    #endregion AI
}
