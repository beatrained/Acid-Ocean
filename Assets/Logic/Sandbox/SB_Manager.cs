using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AcidOcean.Sandbox
{
    public static class SB_Manager
    {
        public static UnityEvent WhenCheckboxMarked = new UnityEvent();

        public static void OnCheckboxMarked() => WhenCheckboxMarked.Invoke();
    }
}
