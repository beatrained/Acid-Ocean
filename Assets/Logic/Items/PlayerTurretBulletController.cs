using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretBulletController : MonoBehaviour
{
    private Vector3 _shootDir;
    private float _shootSpeed;

    public void InitProjectile(Vector3 direction, float speed)
    {
        _shootDir = direction;
        _shootSpeed = speed;
    }

    void Update()
    {
        transform.position += _shootDir * _shootSpeed * Time.deltaTime; 
    }
}
