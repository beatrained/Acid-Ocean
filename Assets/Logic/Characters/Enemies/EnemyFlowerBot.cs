using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlowerBot : EnemyCharacter
{
    [SerializeField] private GameObject _flowerCap;

    private CharStatsManagerEnemies _charStatsManagerEnemies;
    public CharStatsManagerEnemies CharStatsManagerEnemies => _charStatsManagerEnemies;

    //public CharStatsManagerEnemies CharStatsManagerEnemies { get; private set; }
    //public ScrEnemyCharacterMelee ScriptableEnemyCharacter { get; private set; }

    private Animator _animator;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbodyCap;
    private MovingComponent _movingComponent;
    private EnemyFlowerBotBlades _blades;

    private void Awake()
    {
        RunOnAwake();
        _charStatsManagerEnemies = GetComponent<CharStatsManagerEnemies>();
        _animator = GetComponent<Animator>();                               //
        _agent = GetComponent<NavMeshAgent>();                              //
        _movingComponent = GetComponent<MovingComponent>();                 //
        _blades = GetComponentInChildren<EnemyFlowerBotBlades>();           //
        _rigidbodyCap = _flowerCap.GetComponent<Rigidbody>();               // ASK How properly manage this in complex objects?
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

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude); // TODO another .magnitude here
    }

    private void Start()
    {
        ChangeState(ActorState.Sleeping);

        _agent.speed = CharStatsManagerEnemies.CharBasicStats.Speed;


        //DamageCanDealAmount = _scriptableEnemyCharacter.DamageRangeBase.x;
        //print(DamageCanDealAmount + " = DamageCanDealAmount");
    }

    public override void HandleSleeping()
    {
        _blades.SpinBlades = false;
        _blades.OpenOrCloseBlades(false);
        CanIMove = false;
        _movingComponent.MovementChoice = 2;
        print("flower bot is SLEEPING");
    }

    public override void HandleChasing()
    {
        print("flower bot is now CHASING " + TargetToMoveTo.name);
        StartCoroutine(ChasingSequence());
    }

    private IEnumerator ChasingSequence()
    {
        yield return new WaitForSeconds(0.2f);
        RemoveFlowerCap();
        CanIMove = true;
        _movingComponent.MovementChoice = 0;
        _blades.OpenOrCloseBlades(true);
        yield return new WaitForSeconds(0.8f);
        _blades.SpinBlades = true;
    }

    public override void HandleAccuireTarget()
    {
        // deal with player
        if (TargetToMoveTo.CompareTag("Player"))
        {
            ChangeState(ActorState.Chasing);
        }
    }

    public override void HandleDying()
    {

    }

    // here is a bug, this code needs to be called only ONCE, not every time when actor enters Chasing state
    // TODO flowerCap bugfix
    private void RemoveFlowerCap()
    {
        _rigidbodyCap.isKinematic = false;
        _rigidbodyCap.GetComponent<BoxCollider>().enabled = true;
        _rigidbodyCap.AddForce(StaticUtils.RandomVector3(0, 1, 8, 12, 0, 1), ForceMode.Impulse);
        _rigidbodyCap.AddTorque(StaticUtils.RandomVector3(0, 0.5f, 0, 0.5f, 0, 0.5f), ForceMode.Impulse);
        _flowerCap.transform.parent = null;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}