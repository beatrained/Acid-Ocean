using UnityEngine;
using UnityEngine.Events;

namespace AcidOcean.Game
{
    public class GlobalEventManager
    {
        public static event UnityAction<float> PlayerRecieveDamage;
        public static event UnityAction<GameObject> HealthIsEqualsZero;

        public static void PlayerDamaged(float damage) => PlayerRecieveDamage?.Invoke(damage);
        public static void HealthIsZeroved(GameObject obj) => HealthIsEqualsZero?.Invoke(obj);
    }
}