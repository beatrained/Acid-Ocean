using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Player Scriptable Object")]
public class ScrPlayerCharacter : ScrUnitBase
{
    //sth additional exclusively to player stats maybe here?
    [Space(10), Header("Additional Stats")]
    [SerializeField] public float SpeedOnFourLegs = 21;
}
