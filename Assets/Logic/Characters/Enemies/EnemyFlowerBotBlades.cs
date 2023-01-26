using System.Collections;
using AcidOcean.Game;
using UnityEngine;

public class EnemyFlowerBotBlades : MonoBehaviour, IDamaging
{
    private Animator _bladesAnimator;
    EnemyFlowerBot _thisCharacter;
    public bool SpinBlades { get; set; }
    [SerializeField] float _spinningSpeed = 20;
    MeshCollider _bladesCollider;

    CharacterStatsManagerBase _characterStatsManager; //


    private void Awake()
    {
        _thisCharacter = GetComponentInParent<EnemyFlowerBot>();
        _bladesAnimator = GetComponent<Animator>();
        _bladesCollider = GetComponent<MeshCollider>();
        _characterStatsManager = GetComponentInParent<CharacterStatsManagerBase>(); //
    }

    private void Start()
    {
        //DamageAmount = _thisCharacter.DamageCanDealAmount;
    }

    public void OpenOrCloseBlades(bool isOpened)
    {
        if (isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", true);
            StartCoroutine(EnableCollision(1f, true));
        }
        else if (!isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", false);
            StartCoroutine(EnableCollision(1f, false));
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
        IDamageable dam = col.gameObject.GetComponent<IDamageable>();
        
        if (dam != null)
        {
            print("Blades collided with + " + col.gameObject.name + " and deal damage");
            GlobalEventManager.PlayerDamaged(_thisCharacter.CharacterStatsManager.AiStats.MinMaxDamage.x);
            // TODO block check, invert knockback direction if necessary
            Vector3 dirToTarget = (col.transform.position - this.transform.position).normalized;
            ObjectsPositionManipulator
                .KnockbackActor(col.gameObject, dirToTarget * _thisCharacter.CharacterStatsManager.CharBasicStats.KnockbackStrength);
        }
    }

    #region IDamaging
    public float DamageAmount { get; set; }
    public float KnockbackStrength { get; set; }
    public void DealDamage(float amount)   // TODO deal with interface IDamaging
    {
        print("Blade hurts");
    }
    #endregion IDamaging
}