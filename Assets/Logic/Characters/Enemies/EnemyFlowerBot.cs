using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlowerBot : EnemyCharacter
{
    [SerializeField] private GameObject _flowerCap;

    private CharStatsManagerEnemies _charStatsManagerEnemies;
    public CharStatsManagerEnemies CharStatsManagerEnemies => _charStatsManagerEnemies;

    private Animator _animator;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbodyCap;
    private MovingComponent _movingComponent;
    private EnemyFlowerBotBlades _blades;

    private bool _capRemoved = false;

    private void Awake()
    {
        RunOnAwake();
        _charStatsManagerEnemies = GetComponent<CharStatsManagerEnemies>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _movingComponent = GetComponent<MovingComponent>();
        _blades = GetComponentInChildren<EnemyFlowerBotBlades>();
        _rigidbodyCap = _flowerCap.GetComponent<Rigidbody>();
    }

    public override GameObject TargetToMoveTo
    {
        get => base.TargetToMoveTo;
        set
        {
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

    public void StunMePlease()
    {
        ChangeState(ActorState.Stunned);
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude); // TODO another .magnitude here
    }

    private void Start()
    {
        ChangeState(ActorState.Sleeping);
        _agent.speed = CharStatsManagerEnemies.CharBasicStats.Speed;
    }

    public override void HandleSleeping()
    {
        CanIMove = false;
        _blades.SpinBlades = false;
        _blades.UnpackBlades(false);
        _movingComponent.MovementChoice = 2;
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
            StartCoroutine(AsleepSequence());
        }
    }

    public override void HandleStunned()
    {
        StartCoroutine(StunnedSequence());
    }

    private IEnumerator StunnedSequence()
    {
        CanIMove = false;
        _blades.SpinBlades = false;
        // _blades.UnpackBlades(false);
        _animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(1f);  // TODO actor stun time parameter
        _animator.SetBool("Stunned", false);
        ChangeState(ActorState.Chasing);
    }

    public override void HandleAccuireTarget()
    {
        // deal with player
        if (TargetToMoveTo.CompareTag("Player"))
        {
            ChangeState(ActorState.Chasing);
        }
    }
    private IEnumerator AsleepSequence()
    {
        yield return new WaitForSeconds(0.2f);
        RemoveFlowerCap();
        _movingComponent.MovementChoice = 0;
        _blades.UnpackBlades(true);
        yield return new WaitForSeconds(1f);
        _blades.SpinBlades = true;
    }

    public override void HandleDying()
    {

    }

    private void RemoveFlowerCap()
    {
        if (_capRemoved == true)
        {
            return;
        }
        _capRemoved = true;
        _rigidbodyCap.isKinematic = false;
        _rigidbodyCap.GetComponent<BoxCollider>().enabled = true;
        _rigidbodyCap.AddForce(StaticUtils.RandomVector3(0, 1, 8, 12, 0, 1), ForceMode.Impulse);
        _rigidbodyCap.AddTorque(StaticUtils.RandomVector3(0, 0.5f, 0, 0.5f, 0, 0.5f), ForceMode.Impulse);
        _flowerCap.transform.parent = null; // TODO an idea. parent all shit like this to an empty object, then deal with it
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}