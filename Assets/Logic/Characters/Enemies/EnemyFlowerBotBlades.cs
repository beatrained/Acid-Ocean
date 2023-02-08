using System.Collections;
using UnityEngine;

public class EnemyFlowerBotBlades : MonoBehaviour, IDamaging
{
    [SerializeField] private Animator _bladesAnimator;
    private EnemyFlowerBot _thisCharacter;

    public bool SpinBlades { get; set; }

    public float DamageAmount
    {
        get => Random.Range(_thisCharacter.CharacterStatsManager.AiStats.MinMaxDamage.x,
                            _thisCharacter.CharacterStatsManager.AiStats.MinMaxDamage.y);
    }

    [SerializeField] private float _spinningSpeed = 20;
    private MeshCollider _bladesCollider;

    private CharacterStatsManagerBase _characterStatsManager; //

    private void Awake()
    {
        _thisCharacter = GetComponentInParent<EnemyFlowerBot>();
        _bladesAnimator = GetComponent<Animator>();
        _bladesCollider = GetComponent<MeshCollider>();
        _characterStatsManager = GetComponentInParent<CharacterStatsManagerBase>(); //
    }

    public void UnpackBlades(bool isOpened)
    {
        if (isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", true);
            //StartCoroutine(EnableCollision(1f , true));
            _bladesCollider.enabled = true;
        }
        else if (!isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", false);
            //StartCoroutine(EnableCollision(0.2f, false));
            _bladesCollider.enabled = false;
        }
    }

    private void Update()
    {
        if (SpinBlades)
        {
            transform.Rotate(Vector3.up, 45 * Time.deltaTime * _spinningSpeed);
        }
    }

    private IEnumerator EnableCollision(float delay, bool position)
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
        if (col.gameObject.tag != "Player")
        {
            return;
        }
        // TODO more universal hit method ?
        IDamageable dam = col.gameObject.GetComponent<IDamageable>();
        if (dam == null) return;
        bool isBlocked = col.gameObject.GetComponent<Animator>().GetBool("Block");
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
                ObjectsPositionManipulator
                    .KnockbackActor(col.gameObject, dirToTarget * _thisCharacter.CharacterStatsManager.CharBasicStats.KnockbackStrength);
            }
        }
    }
}