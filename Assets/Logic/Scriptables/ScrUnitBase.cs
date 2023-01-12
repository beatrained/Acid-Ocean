using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrUnitBase : ScriptableObject
{
    [SerializeField] private BasicActorStats _stats;
    public BasicActorStats BaseStats => _stats;
    [SerializeField] private Faction _faction;
    public Faction Faction => _faction;
}

[Serializable]
public struct BasicActorStats
{
    public float Health;
    public float Speed;
    public float SpeedMlt;
    public string Name;
}

[Serializable]
public enum Faction
{
    PlayerCharacters = 0,
    EnemyCharacters = 1,
}
