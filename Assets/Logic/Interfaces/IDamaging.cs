using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaging
{
    float DamageAmount { get; set; }
    void DealDamage(float amount);
}
