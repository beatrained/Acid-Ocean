using AcidOcean.Sandbox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_Listener : MonoBehaviour
{

    private void Awake()
    {
        // ����� ������� �������.
        SB_Manager.WhenCheckboxMarked.AddListener(CheckboxMarked);
    }

    private void CheckboxMarked()
    {
        print("Checkbox Marked!");
    }
}
