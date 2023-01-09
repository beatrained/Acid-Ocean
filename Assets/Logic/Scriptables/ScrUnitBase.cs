using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrUnitBase : ScriptableObject
{
    [SerializeField] private Stats _stats;
    public Stats BaseStats => _stats;
    [SerializeField] private Faction _faction;
    public Faction Faction => _faction;
}

[Serializable]
public struct Stats
{
    public float Health;
    public float Speed;
    public float SpeedMlt;

    public Stats(float health, float speed, float speedMlt)
    {
        Health = health;
        Speed = speed;
        SpeedMlt = speedMlt;
    }
}

[Serializable]
public enum Faction
{
    PlayerCharacters = 0,
    EnemyCharacters = 1,
}
