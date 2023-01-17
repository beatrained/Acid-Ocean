using UnityEngine;
using UnityEngine.Events;


namespace AcidOcean.Game
{
    public static class LocalEventManager
    {
        public static event UnityAction StandOnTwoLegs;
        public static event UnityAction StandOnFourLegs;
        public static void StayOnTwo() => StandOnTwoLegs?.Invoke();
        public static void StayOnFour() => StandOnFourLegs?.Invoke();

    }
}
