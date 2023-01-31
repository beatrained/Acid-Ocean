using AcidOcean.Game;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlowerBot : EnemyCharacter
{
    private CharStatsManagerEnemies _charStatsManagerEnemies;
    public CharStatsManagerEnemies CharStatsManagerEnemies => _charStatsManagerEnemies;

    [SerializeField] private GameObject _flowerCap;
    [SerializeField] private Renderer _healthIndicator;
    private ParticleSystem _stunParticles;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbodyCap;
    private MovingComponent _movingComponent;
    private EnemyFlowerBotBlades _blades;

    private bool _capRemoved = false;
    private float _stunForTime = 1;
    private bool _isStunParticles = false;
    private byte _defaultMoveScheme = 0;

    private Collider[] _legsColliders;
    private Rigidbody[] _legsRigidbodies;

    private void Awake()
    {
        RunOnAwake();
        //GlobalEventManager.HealthIsEqualsZero += GlobalEventManager_HealthIsEqualsZero;

        _stunParticles = GetComponentInChildren<ParticleSystem>();

        _charStatsManagerEnemies = GetComponent<CharStatsManagerEnemies>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _movingComponent = GetComponent<MovingComponent>();
        _blades = GetComponentInChildren<EnemyFlowerBotBlades>();
        _rigidbodyCap = _flowerCap.GetComponent<Rigidbody>();

        _legsColliders = GetComponentsInChildren<Collider>();
        _legsRigidbodies = GetComponentsInChildren<Rigidbody>();
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

    public void StunMePlease(float time, bool enableParticles)
    {
        _isStunParticles = enableParticles;
        _stunForTime = time;
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
        StartCoroutine(StunnedSequence());
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
        // TODO not here
        var healthPercent = 1 - _charStatsManagerEnemies.CharBasicStats.Health / _charStatsManagerEnemies.CharScriptable.BasicStats.Health;
        _healthIndicator.material.SetFloat("_HealthPercent", healthPercent);
        //StunMePlease(0.5f, false);
        //_charStatsManagerEnemies.TakeDamage(_charStatsManagerEnemies.IncomingDamage);
    }

    public override void HandleDying()
    {
        StartCoroutine(DeathSequence());
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
        _flowerCap.transform.parent = null; // TODO parent all shit like this to an empty object, then deal with it
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

    private IEnumerator StunnedSequence()
    {
        if (_isStunParticles)
        {
            _stunParticles.Play();
        }

        CanIMove = false;
        _movingComponent.MovementChoice = 2;
        _blades.SpinBlades = false;
        _blades.UnpackBlades(false);
        _animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(_stunForTime);          // TODO actor stun time parameter or sth
        _animator.SetBool("Stunned", false);
        ChangeState(ActorState.Chasing);
    }

    private IEnumerator DeathSequence()
    {
        _blades.SpinBlades = false;
        _blades.UnpackBlades(false);
        Destroy(_blades.GetComponent<Rigidbody>());

        CanIMove = false;
        _movingComponent.MovementChoice = 2;
        TargetToMoveTo = null;
        GetComponent<VisionComponent>().enabled = false;
        GetComponent<MovingComponent>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        _animator.enabled = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        foreach (Collider col in _legsColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rgb in _legsRigidbodies)
        {
            rgb.isKinematic = false;
        }
        yield return new WaitForSeconds(1f);
        // some effects mb
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