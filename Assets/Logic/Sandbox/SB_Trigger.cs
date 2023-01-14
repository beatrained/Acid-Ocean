using AcidOcean.Sandbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_Trigger : MonoBehaviour
{
    [SerializeField] private bool _manualTrigger = false;
    Vector3 _testVector = new Vector3(1, 0, 1);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Событие происходит в этом классе. Срабатываем
        if (_manualTrigger)
        {
            SB_Manager.WhenCheckboxMarked.Invoke();
        }
    }
}
