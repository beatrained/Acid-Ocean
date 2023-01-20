using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Player Scriptable Object")]
public class ScrPlayerCharacter : ScrUnitBase
{
    //sth additional exclusively to player stats maybe here?
    [Space(10), Header("Additional Player Stats")]
    [SerializeField] private float _speedOnFourLegs = 21;

    public float SpeedOnFourLegs => _speedOnFourLegs;
}
