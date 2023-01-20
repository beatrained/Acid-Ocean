using UnityEngine.Events;

namespace AcidOcean.Game
{
    public class GlobalEventManager
    {
        public static event UnityAction<float> PlayerRecieveDamage;

        public static void PlayerDamaged(float damage) => PlayerRecieveDamage?.Invoke(damage);
    }
}