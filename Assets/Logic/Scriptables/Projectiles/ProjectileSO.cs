using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "ScriptableObjects/ProjectileSO")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] private string _projectileName;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _projectileSpeed;

    public string ProjectileName => _projectileName;
    public GameObject ProjectilePrefab => _projectilePrefab;
    public float FireRate => _fireRate;
    public float ProjectileSpeed => _projectileSpeed;
}
