using System.Collections;
using AcidOcean.Game;
using UnityEngine;

public class EnemyFlowerBotBlades : MonoBehaviour, IDamaging
{
    [SerializeField] private Animator _bladesAnimator;
    EnemyFlowerBot _thisCharacter;
    public bool SpinBlades { get; set; }
    public float DamageAmount { get; set; }

    [SerializeField] float _spinningSpeed = 20;
    MeshCollider _bladesCollider;

    CharacterStatsManagerBase _characterStatsManager; //


    private void Awake()
    {
        _thisCharacter = GetComponentInParent<EnemyFlowerBot>();
        //_bladesAnimator = GetComponent<Animator>();
        _bladesCollider = GetComponent<MeshCollider>();
        _characterStatsManager = GetComponentInParent<CharacterStatsManagerBase>(); //
    }

    private void Start()
    {
        //DamageAmount = _thisCharacter.DamageCanDealAmount;
    }

    public void UnpackBlades(bool isOpened)
    {
        if (_bladesAnimator == null)
        {
            print(gameObject.name + " reports that his BLADES ANIMATOR == null");
            return;
        }
        if (isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", true);
            StartCoroutine(EnableCollision(1f, true));
        }
        else if (!isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", false);
            StartCoroutine(EnableCollision(0.2f, false));
        }
    }

    private void Update()
    {
        if (SpinBlades)
        {
            transform.Rotate(Vector3.up, 45  * Time.deltaTime * _spinningSpeed);
        }
    }

    IEnumerator EnableCollision(float delay, bool position)
    {
        yield return new WaitForSeconds(delay);
        _bladesCollider.enabled = position;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision col)
    {
        // TODO (targets system). Temporary solution:
        if (col.gameObject.name != "Cr_Player")
        {
            return;
        }
        // TODO more universal hit method
        IDamageable dam = col.gameObject.GetComponent<IDamageable>();
        bool isBlocked = col.gameObject.GetComponent<Animator>().GetBool("Block");
        if (dam != null)
        {
            Vector3 dirToTarget = StaticUtils.DirToTarget(col.transform.position, this.transform.position);
            if (isBlocked)
            {
                ObjectsPositionManipulator
                .KnockbackActor(_thisCharacter.gameObject, -dirToTarget * _thisCharacter.CharacterStatsManager.CharBasicStats.KnockbackStrength);
                _thisCharacter.StunMePlease(2, true);
            }
            else
            {
                GlobalEventManager.PlayerDamaged(Random
                    .Range(_thisCharacter.CharacterStatsManager.AiStats.MinMaxDamage.x, _thisCharacter.CharacterStatsManager.AiStats.MinMaxDamage.y));
                ObjectsPositionManipulator
                    .KnockbackActor(col.gameObject, dirToTarget * _thisCharacter.CharacterStatsManager.CharBasicStats.KnockbackStrength);
            }
        }
    }
}