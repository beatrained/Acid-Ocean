using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySOMelee", menuName = "ScriptableObjects/Enemy Scriptable Object (melee)")]
public class ScrEnemyCharacterMelee : ScrUnitBase
{
    [Space(10), Header("Additional Stats")]
    [SerializeField] float _agroRange;

    public float AgroRange => _agroRange;
}
