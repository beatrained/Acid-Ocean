using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBot : EnemyCharacter
{

    private byte _defaultMoveScheme = 1;
    private float _agentVelocity;
    private bool _isAnimActive = false;
    [SerializeField] private float _xRotationSpeed = 20;
    [SerializeField] private float _yRotationSpeed = 0.5f;

    private void Awake()
    {
        RunOnAwake();
    }

    private void Start()
    {
        ChangeState(ActorState.Sleeping);
        ThisAgent.speed = ThisCharStatManagerEnemies.CharBasicStats.Speed;
    }

    private void Update()
    {
        _agentVelocity = ThisAgent.velocity.magnitude / ThisAgent.speed;

        Vector3 lookDirection = ThisMovingComponent.TempTargetPoint - transform.position;
        Quaternion rotation = Quaternion
            .LookRotation(lookDirection, Vector3.up) * Quaternion.Euler(new Vector3(_agentVelocity * _xRotationSpeed, 0,0));   
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _yRotationSpeed);

        if (!_isAnimActive)
        {
            StartCoroutine(RandomSwings());
        }
    }

    private IEnumerator RandomSwings()
    {
        _isAnimActive = true;
        yield return new WaitForSeconds(Random.Range(6, 14));
        ThisAnimator.SetTrigger("Swing");
        _isAnimActive = false;
    }

    public override void HandleSleeping()
    {
        if (TargetToMoveTo != null)
        {
            ChangeState(ActorState.AccuireTarget);
        }
        else
        {
            CanIMove = false;
            ThisMovingComponent.MovementChoice = 2;
        }
    }

    public override void HandleAccuireTarget()
    {
        // deal with player
        if (TargetToMoveTo.CompareTag("Player"))
        {
            ThisMovingComponent.MovementChoice = _defaultMoveScheme;
            ChangeState(ActorState.Chasing);
        }
    }

    public override void HandleChasing()
    {
        CanIMove = true;
        if (TargetToMoveTo == null)
        {
            ChangeState(ActorState.Sleeping);
        }
        else
        {
            ThisMovingComponent.MovementChoice = _defaultMoveScheme;
        }
    }
}
