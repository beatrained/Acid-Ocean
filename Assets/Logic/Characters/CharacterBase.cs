using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterBase : MonoBehaviour
{
    // TODO scriptable in base class
    // [SerializeField] public ScrUnitBase ActorScriptableObject;

    public ScrUnitBase CharStats { get; set; }

    public virtual void SetStats(ScrUnitBase stats)
    {
        CharStats = stats;
    }

    public virtual void ApplyDamage(float damage)
    { } // temp mb in interface?

    public float CosVisionConeAngle()
    {
        return Mathf.Cos(CharStats.VisionAngle * Mathf.Deg2Rad);
    }

    //===============================================

    #region AI

    public bool CanIMove = false;
    public virtual GameObject TargetToMoveTo { get; set; }
    public ActorState CurrentState { get; private set; }

    public event UnityAction<ActorState> ActorStateChanged;

    public virtual void CanSeeTarget(DetectableTargetComponent target)
    {
        print("I can see " + target.gameObject.name);
    }

    private protected void ChangeState(ActorState newState)
    {
        if (newState == CurrentState)
        {
            return;
        }
        CurrentState = newState;
        switch (newState)
        {
            case ActorState.Sleeping:
                HandleSleeping();
                break;

            case ActorState.Wandering:
                HandleWandering();
                break;

            case ActorState.AccuireTarget:
                HandleAccuireTarget();
                break;

            case ActorState.Chasing:
                HandleChasing();
                break;

            case ActorState.Attacking:
                HandleAttacking();
                break;

            case ActorState.TakingDamage:
                HandleTakingDamage();
                break;

            case ActorState.Spawning:
                HandleSpawning();
                break;

            case ActorState.Dying:
                HandleDying();
                break;
        }
        ActorStateChanged?.Invoke(CurrentState);
    }

    public virtual void HandleDying()
    {
        print("Dying...");
    }

    public virtual void HandleSpawning()
    {
        print("Spawning...");
    }

    public virtual void HandleTakingDamage()
    {
        print("Being hit!");
    }

    public virtual void HandleAttacking()
    {
        print("Attacking!");
    }

    public virtual void HandleAccuireTarget()
    {
        print("Target confirmed! What can I do with it?");
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