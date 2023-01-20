using UnityEngine;
using UnityEngine.Events;


namespace AcidOcean.Game
{
    public static class LocalEventManager
    {
        public static event UnityAction StandOnTwoLegs;
        public static event UnityAction StandOnFourLegs;

        public static event UnityAction<GameObject> HealthIsEqualsZero;
        public static void StayOnTwo() => StandOnTwoLegs?.Invoke();
        public static void StayOnFour() => StandOnFourLegs?.Invoke();
        public static void HealthIsZeroved(GameObject obj) => HealthIsEqualsZero?.Invoke(obj);

    }
}
