using UnityEngine;

[CreateAssetMenu(fileName = "EnemySOMelee", menuName = "ScriptableObjects/Enemy Scriptable Object (melee)")]
public class ScrEnemyCharacter : ScrUnitBase
{
    [Space(10), Header("Additional Stats")]
    [SerializeField] float _agroRange;
    [SerializeField] EnemyType _enemyType;

    public float AgroRange => _agroRange;
    public EnemyType EnemyType => _enemyType;
}

public enum EnemyType
{
    FlowerBot,
    RakeBot,
    RangeBot,
}
