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

    private void OnDisable()
    {
        DetectableTargetManager.Instance.Unregister(this);
    }
}
