using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script is on dummy rigidbody tester

public class sandbox_234231 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ObjectsPositionManipulator.KnockbackActor(gameObject, new Vector3(8, 4, 8));
        }
    }
}
