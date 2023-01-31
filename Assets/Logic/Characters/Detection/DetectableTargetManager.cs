using System.Collections.Generic;
using UnityEngine;

public class DetectableTargetManager : MonoBehaviour
{
    public static DetectableTargetManager Instance { get; private set; } = null;
    public List<DetectableTargetComponent> AllTargets { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        AllTargets = new List<DetectableTargetComponent>();
        //print("Awake is done `````````````````````````````````````");
    }

    public void Register(DetectableTargetComponent target)
    {
        AllTargets.Add(target);
    }

    public void Unregister(DetectableTargetComponent target)
    {
        AllTargets.Remove(target);
    }
}
