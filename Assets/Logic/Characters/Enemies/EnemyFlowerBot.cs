using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlowerBot : EnemyCharacter
{

    [SerializeField] private ScrEnemyCharacterMelee _scriptableEnemyCharacter;
    public ScrEnemyCharacterMelee ScriptableEnemyCharacter { get; private set; }

    Animator _animator;
    NavMeshAgent _agent;
    MovingComponent _movingComponent;

    

    private void Awake()
    {
        ChangeState(ActorState.Sleeping);
        SetStats(_scriptableEnemyCharacter);
    }

    // we need to calculate agent speed

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude); // TODO another .magnitude here
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _movingComponent = GetComponent<MovingComponent>();
        _agent.speed = CharStats.Speed;
    }

    public override void HandleSleeping()
    {
        //base.HandleSleeping();
        print("flower bot is SLEEPING");
    }

    public override void HandleChasing()
    {
        print("flower bot is now CHASING " + TargetToMoveTo.name);
        CanIMove = true;
        _movingComponent.MovementChoice = 1;
    }

    public override void HandleTarget()
    {
        // deal with player
        if (TargetToMoveTo.CompareTag("Player"))
        {
            ChangeState(ActorState.Chasing);
        }
    }
}
