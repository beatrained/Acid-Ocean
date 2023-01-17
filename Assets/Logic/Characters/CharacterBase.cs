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
    public GameObject TargetToMoveTo { get; set; }
    public ActorState CurrentState { get; private set; }

    public event UnityAction<ActorState> ActorStateChanged;

    public virtual void CanSeeTarget(DetectableTargetComponent target)
    {
        print("I can see " + target.gameObject.name);
    }

    public void ChangeState(ActorState newState)
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
                HandleTarget();
                break;

            case ActorState.Chasing:
                HandleChasing();
                break;

            case ActorState.Attacking:
                HandleAttacking();
                break;
        }
        ActorStateChanged?.Invoke(CurrentState);
    }

    public virtual void HandleAttacking()
    {
        print("Attacking!");
    }

    public virtual void HandleTarget()
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