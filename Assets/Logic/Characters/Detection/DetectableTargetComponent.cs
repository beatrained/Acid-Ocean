using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableTargetComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DetectableTargetManager.Instance.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        DetectableTargetManager.Instance.Unregister(this);
    }
}
