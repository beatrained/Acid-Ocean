using AcidOcean.Game;
using System.Collections;
using Unity.VisualScripting;
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

    private ParticleSystem _stunParticles;

    private bool _capRemoved = false;
    private float _stunForTime = 1;       //
    private byte _defaultMoveScheme = 0;

    private void Awake()
    {
        RunOnAwake();
        GlobalEventManager.HealthIsEqualsZero += GlobalEventManager_HealthIsEqualsZero;

        _stunParticles = GetComponentInChildren<ParticleSystem>();

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

    private void GlobalEventManager_HealthIsEqualsZero(GameObject arg)
    {
        if (arg == gameObject)
        {
            ChangeState(ActorState.Dying);
        }
    }

    public void StunMePlease(float time)
    {
        _stunForTime = time;
        ChangeState(ActorState.Stunned);
    }

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude); // TODO another .magnitude here
        
        
        
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            print("Can I Move = " + CanIMove);
        }



    }

    private void Start()
    {
        ChangeState(ActorState.Sleeping);
        _agent.speed = CharStatsManagerEnemies.CharBasicStats.Speed;
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
            _blades.SpinBlades = false;
            _blades.UnpackBlades(false);
            _movingComponent.MovementChoice = 2;
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
            StartCoroutine(AsleepSequence());
        }
    }

    public override void HandleStunned()
    {
        StartCoroutine(StunnedSequence(_stunForTime));
    }

    public override void HandleAccuireTarget()
    {
        // deal with player
        if (TargetToMoveTo.CompareTag("Player"))
            _movingComponent.MovementChoice = _defaultMoveScheme;
        {
            ChangeState(ActorState.Chasing);
        }
    }

    public override void HandleTakingDamage()
    {
        _charStatsManagerEnemies.TakeDamage(_charStatsManagerEnemies.IncomingDamage);
    }

    public override void HandleDying()
    {
        gameObject.SetActive(false);
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

    private IEnumerator AsleepSequence()
    {
        yield return new WaitForSeconds(0.2f);
        RemoveFlowerCap();

        _blades.UnpackBlades(true);
        yield return new WaitForSeconds(0.4f);
        _blades.SpinBlades = true;
        _movingComponent.MovementChoice = _defaultMoveScheme;
    }

    private IEnumerator StunnedSequence(float stunTime)
    {
        _stunParticles.Play();

        CanIMove = false;
        _movingComponent.MovementChoice = 2;
        _blades.SpinBlades = false;
        _blades.UnpackBlades(false);
        _animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunTime);          // TODO actor stun time parameter or sth
        _animator.SetBool("Stunned", false);
        ChangeState(ActorState.Chasing);
    }

    private void OnEnable()
    {
        ChangeState(ActorState.Sleeping);
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