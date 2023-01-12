using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrUnitBase : ScriptableObject
{
    [Header("Main Actor Stats")]
    [SerializeField] public float Health;
    [SerializeField] public float Speed;
    [SerializeField] public float SpeedMlt;
    [SerializeField] public string Name;
    [SerializeField] private Faction _faction;
    [SerializeField] public Faction Faction => _faction;
}

[Serializable]
public enum Faction
{
    PlayerCharacters = 0,
    EnemyCharacters = 1,
}
