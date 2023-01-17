using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableTargetComponent : MonoBehaviour
{
    private void OnEnable()
    {
        DetectableTargetManager.Instance.Register(this);
    }

    private void OnDestroy()
    {
        DetectableTargetManager.Instance.Unregister(this);
    }
}
