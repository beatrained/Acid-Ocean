using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : CharacterBase
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;
    private Animator _animator;
    private MovingComponent _movingComponent;
    private CharStatsManagerEnemies _charStatsManagerEnemies;

    [SerializeField] public Renderer HealthIndicator;

    public Rigidbody ThisRigidbody { get { return _rigidbody; } }
    public NavMeshAgent ThisAgent { get { return _agent; } }
    public Animator ThisAnimator { get { return _animator; } }
    public MovingComponent ThisMovingComponent { get { return _movingComponent; } }
    public CharStatsManagerEnemies ThisCharStatManagerEnemies { get { return _charStatsManagerEnemies; } }

    public override void RunOnAwake()
    {
        base.RunOnAwake();
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _movingComponent = GetComponent<MovingComponent>();
        _charStatsManagerEnemies = GetComponent<CharStatsManagerEnemies>();
    }

    public override GameObject TargetToMoveTo
    {
        get => base.TargetToMoveTo;
        set
        {
            if (value == base.TargetToMoveTo)
            {
                return;
            }
            base.TargetToMoveTo = value;
            if (value != null)
            {
                ChangeState(ActorState.AccuireTarget);
            }
            else if (value == null)
            {
                ChangeState(ActorState.Sleeping);
            }
        }
    }
}
