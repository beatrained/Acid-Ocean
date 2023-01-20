using JetBrains.Annotations;
using System;
using UnityEngine;

public abstract class ScrUnitBase : ScriptableObject
{
    [Header("Main Actor Stats")]
    //[SerializeField] private float _health;
    //[SerializeField] private float _speed;
    //[SerializeField] private float _speedMlt;
    //[SerializeField] private string _charName;
    //[SerializeField] private Vector2 _minMaxDamage;

    //// AI
    //[SerializeField] private Faction _faction;
    //[SerializeField] private float _visionRange;
    //[SerializeField] private float _visionAngle;
    //[SerializeField] private float _hearingRange;

    [SerializeField] BasicStats _basicStats;

    [Header("AI Stats")]
    [SerializeField] AIStats _aiStats;

    public BasicStats BasicStats => _basicStats;
    public AIStats AIStats => _aiStats;


    //[SerializeField] public Faction Faction => _faction;
    //public float Health { get => _health; set => _health = value; }
    //public float Speed { get => _speed; set => _speed = value; }
    //public float SpeedMlt { get => _speedMlt; set => _speedMlt = value; }
    //public string Name { get => _charName; set => _charName = value; }
    //public Vector2 DamageRangeBase { get => damageRangeBase; set => damageRangeBase = value; }
    //public float VisionRange { get => _visionRange; set => _visionRange = value; }
    //public float VisionAngle { get => _visionAngle; set => _visionAngle = value; }
    //public float HearingRange { get => _hearingRange; set => _hearingRange = value; }
}

[Serializable]
public struct BasicStats
{
    public string CharName;
    public float Health;
    public float Speed;
    public float SpeedMlt;
    public Faction Faction;
}

[Serializable]
public struct AIStats
{
    public Vector2 MinMaxDamage;
    public float VisionRange;
    public float VisionAngle;
    public float HearingRange;
}

[Serializable]
public enum Faction
{
    PlayerCharacters = 0,
    EnemyCharacters = 1,
}
