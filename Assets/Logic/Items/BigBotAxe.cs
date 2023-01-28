using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBotAxe : MonoBehaviour, IDamaging
{
    //collider that enabled or not
    //shield activation on specific animation
    //axe animations in general
    private Animator _animator, _parentAnimator;
    private GameObject _shield;
    private Collider _collider;
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _parentAnimator = GetComponentInParent<Animator>();
        _collider = GetComponent<Collider>();
        _shield = transform.GetChild(0).gameObject;
    }

    public bool IsShieldEnabled
    {
        get { return _shield.activeSelf; }
        set { _shield.SetActive(value); }
    }

    // animation event in clip
    public void _EnableCollisionDamage()
    {
        _collider.enabled = !_collider.enabled;
    }
}
