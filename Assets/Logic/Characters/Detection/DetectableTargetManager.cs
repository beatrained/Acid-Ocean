using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableTargetManager : MonoBehaviour
{
    public static DetectableTargetManager Instance { get; private set; } = null;
    [SerializeField] public List<DetectableTargetComponent> AllTargets { get; private set; } = new List<DetectableTargetComponent>();

    private void Awake()
    {
        Instance = this;
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