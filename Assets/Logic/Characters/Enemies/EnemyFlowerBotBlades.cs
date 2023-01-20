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

    public float DamageAmount { get; set; }

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
        if (col.gameObject.tag != "Damagamble" && col.gameObject.tag != "Player")
        {
            print("collided with some shit.");
            return;
        }
        IDamageable dam = col.gameObject.GetComponent<IDamageable>();
        if (dam != null)
        {
            print("PLAYER!!! collided with + " + col.gameObject.name);
            GlobalEventManager.PlayerDamaged(_thisCharacter.CharacterStatsManager.AiStats.MinMaxDamage.x);
        }
        else print("IDamaging is null");
    }

    #region IDamaging
    public void DealDamage(float amount)
    {
        print("Blade hurts");
    }
    #endregion IDamaging
}