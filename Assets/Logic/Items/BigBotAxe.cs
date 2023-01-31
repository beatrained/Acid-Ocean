using UnityEngine;

public class BigBotAxe : MonoBehaviour, IDamaging
{
    //collider that enabled or not
    //shield activation on specific animation
    //axe animations in general
    private Animator _animator, _parentAnimator;
    private GameObject _shield;
    private Collider _collider;
    private CharStatsManagerPlayer _charStatManagerPlayer;

    [SerializeField] private LayerMask _layerMask;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _parentAnimator = GetComponentInParent<Animator>();
        _collider = GetComponent<Collider>();
        _charStatManagerPlayer = GetComponentInParent<CharStatsManagerPlayer>();
        _shield = transform.GetChild(1).gameObject;
    }

    public bool IsShieldEnabled
    {
        get { return _shield.activeSelf; }
        set { _shield.SetActive(value); }
    }

    public bool IsColliderEnabled
    {
        get { return _collider.enabled; }
        set { _collider.enabled = value; }
    }

    public float DamageAmount { get => _charStatManagerPlayer.AiStats.MinMaxDamage.x; set => DamageAmount = value; }

    private void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.layer != _layerMask)
        //{
        //    return;
        //}
        //print("%%%%%%%%%%%%%%COLLIDED!");

        if (col.gameObject.GetComponent<IDamageable>() != null && col.gameObject.name == "EnemyBladeFlower_rd") //TODO shit here
        {
            if (col.gameObject.GetComponent<CharStatsManagerEnemies>().CharBasicStats.Faction == Faction.EnemyCharacters)
            {
                col.gameObject.GetComponent<CharStatsManagerEnemies>().TakeDamage(DamageAmount); // здесь нужно отправлять в соотв стейт, а там уже урон считать
                col.gameObject.GetComponent<CharacterBase>().HurtMePlease();
                Vector3 dirToTarget = StaticUtils.DirToTarget(col.transform.position, this.transform.position);
                ObjectsPositionManipulator
                    .KnockbackActor(col.gameObject, dirToTarget * _charStatManagerPlayer.CharBasicStats.KnockbackStrength);
            }
        }
    }
}
