using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//��� ��� ������ ����: ����� �������� ���������� ����� ��������, ��������� � ���

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(Rigidbody))]
public class MovingComponent : MonoBehaviour
{
    private CharacterBase _thisCharacter;
    private NavMeshAgent _agent;

    private Vector3 _tempTargetPoint;
    private Vector3 _offsetToTarget;
    private bool _step = true;

    private delegate IEnumerator MovementDecisions();

    private MovementDecisions WhatToDo;
    int _movementChoice = 0;

    private void Awake()
    {
        _thisCharacter = GetComponent<CharacterBase>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public int MovementChoice       // NOTE make List instead?
    {
        get
        {
            return _movementChoice;
        }
        set
        {
            _movementChoice = Mathf.Clamp(value, 0, 3);
            switch (_movementChoice)
            {
                case 0: WhatToDo = ComeCloserWithOffset;
                    break;
                case 1: WhatToDo = StayAtDistance;
                    break;
                case 2: WhatToDo = StayAlertAndDoNothing;
                    break;
                case 3: WhatToDo = ComeCloserDoomStyle;
                    break;
                default: WhatToDo = StayAlertAndDoNothing;
                    break;
            }
        }
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

    // Go towards target in melee range
    private IEnumerator ComeCloserWithOffset()
    {
        _agent.isStopped = false;
        _step = false;
        _offsetToTarget = StaticUtils.RandomVector3(0.5f, 2, 0, 0, 0.5f, 2);
        _tempTargetPoint = _thisCharacter.TargetToMoveTo.transform.position + _offsetToTarget;
        _agent.destination = _tempTargetPoint;
        yield return new WaitForSeconds(1);
        _agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        _step = true;
    }

    // Go towards target in melee range, another variant, slower, jerky movements 
    private IEnumerator ComeCloserDoomStyle()
    {
        _agent.isStopped = false;
        _step = false;
        _offsetToTarget = StaticUtils.RandomVector3(2,4,0,0,2,4);
        _tempTargetPoint = (transform.position + (5 * (_thisCharacter.TargetToMoveTo.transform.position - transform.position).normalized)) + _offsetToTarget;
        _agent.destination = _tempTargetPoint;
        yield return new WaitForSeconds(0.7f);
        _step = true;
        yield return new WaitForSeconds(0.3f);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}