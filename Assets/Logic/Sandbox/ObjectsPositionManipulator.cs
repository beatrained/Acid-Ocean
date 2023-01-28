using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// extra methods
// all about specific movements / applying force to objects / teleportation / chain reactions /etc
public static class ObjectsPositionManipulator
{
    // TODO some knockback resistance will be nice +++
    public static void KnockbackActor(GameObject target, Vector3 force)
    {
        target.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
