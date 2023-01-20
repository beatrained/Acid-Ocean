using UnityEngine;
using UnityEngine.Events;
using AcidOcean.Game;

[RequireComponent(typeof(CharacterStatsManagerBase))]
public abstract class CharacterBase : MonoBehaviour
{
    public CharacterStatsManagerBase CharacterStatsManager;

    public void RunOnAwake()
    {
        LocalEventManager.HealthIsEqualsZero += LocalEventManager_HealthIsEqualsZero;
        CharacterStatsManager = GetComponent<CharacterStatsManagerBase>();
    }

    private void LocalEventManager_HealthIsEqualsZero(GameObject sended)
    {
        if (sended == gameObject)
        {
            ChangeState(ActorState.Dying);
        }
    }

    public float CosVisionConeAngle()
    {
        return Mathf.Cos(CharacterStatsManager.AiStats.VisionAngle * Mathf.Deg2Rad);
    }

    //========================================================
    // At some point player character will recieve AI controls
    #region ==================AI===================

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
        print(gameObject.name + " is in ActorState.Dying state");
    }

    public virtual void HandleSpawning()
    {
        print(gameObject.name + " is in ActorState.Spawning state");
    }

    public virtual void HandleTakingDamage()
    {
        print(gameObject.name + " is in ActorState.TakingDamage state");
    }

    public virtual void HandleAttacking()
    {
        print(gameObject.name + " is in ActorState.Attacking state");
    }

    public virtual void HandleAccuireTarget()
    {
        print(gameObject.name + " is in ActorState.AccuireTarget state");
    }

    public virtual void HandleChasing()
    {
        print(gameObject.name + " is in ActorState.Chasing state");
    }

    public virtual void HandleWandering()
    {
        print(gameObject.name + " is in ActorState.Wandering state");
    }

    public virtual void HandleSleeping()
    {
        print(gameObject.name + " is in ActorState.Sleeping state");
    }

    #endregion ==================AI===================
}