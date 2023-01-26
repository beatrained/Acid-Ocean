using JetBrains.Annotations;
using System;
using UnityEngine;

public abstract class ScrUnitBase : ScriptableObject
{
    [SerializeField] BasicStats _basicStats;
    [SerializeField] AIStats _aiStats;

    public BasicStats BasicStats => _basicStats;
    public AIStats AIStats => _aiStats;
}

[Serializable]
public struct BasicStats
{
    public string CharName;
    public float Health;
    public float Speed;
    public float SpeedMlt;
    public float KnockbackStrength;
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
