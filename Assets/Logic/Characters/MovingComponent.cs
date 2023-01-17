using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//что тут должно быть: класс получает координаты точки интереса, двигается к ней

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(Rigidbody))]
public class MovingComponent : MonoBehaviour
{
    private CharacterBase _thisCharacter;
    private NavMeshAgent _agent;

    private Vector3 _tempTargetPoint;
    private bool _step = true;
    //private float _distanceToFinalTarget;

    private delegate IEnumerator MovementDecisions();

    private MovementDecisions WhatToDo;
    int _movementChoice = 0;
    public int MovementChoice
    {
        get
        {
            return _movementChoice;
        }
        set
        {
            _movementChoice = Mathf.Clamp(value, 0, 2);
            switch (_movementChoice)
            {
                case 0: WhatToDo = StartZWalking;
                    break;
                case 1: WhatToDo = StayAtDistance;
                    break;
                case 2: WhatToDo = StayAlertAndDoNothing;
                    break;
                default: WhatToDo = StayAlertAndDoNothing;
                    break;
            }
        }
    }

    private void Awake()
    {
        _thisCharacter = GetComponent<CharacterBase>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (_thisCharacter.CanIMove == true && _thisCharacter.TargetToMoveTo != null)
        {
            if (_step)
            {
                StartCoroutine(WhatToDo());
            }
            Debug.DrawLine(_thisCharacter.transform.position, _tempTargetPoint, new Color(0, 255, 0));
        }
    }

    //    _tempPoint = transform.position + (5 * (target - transform.position).normalized);
    //    _distanceToFinalTarget = Mathf.Abs((transform.position - target).sqrMagnitude);


    // go towards target on some distance and hold position (shooting distance for example)
    private IEnumerator StayAtDistance()
    {
        _step = false;
        _tempTargetPoint = _thisCharacter.TargetToMoveTo.transform.position;
        if ((transform.position - _tempTargetPoint).sqrMagnitude > 40)             // TODO remove magic numbers
        {
            _agent.destination = _tempTargetPoint;
            _agent.isStopped = false;
        }
        else
        {
            _agent.isStopped = true;
        }
        yield return null;
        _step = true;
    }

    private IEnumerator StayAlertAndDoNothing()
    {
        print("really? " + _thisCharacter.gameObject.name + " reports that can see target but stay at place");
        yield return new WaitForSeconds(1);
    }

    // Go towards target in melee range, robot-style
    private IEnumerator StartZWalking()
    {
        _agent.isStopped = false;
        _step = false;
        _tempTargetPoint = _thisCharacter.TargetToMoveTo.transform.position + new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3));
        _agent.destination = _tempTargetPoint;
        yield return new WaitForSeconds(1);
        _agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        _step = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}