using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//что тут должно быть: класс получает координаты точки интереса, двигаетс€ к ней

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(Rigidbody))]
public class MovingComponent : MonoBehaviour
{
    private CharacterBase _thisCharacter;
    private NavMeshAgent _agent;

    Vector3 _tempTargetPoint;
    bool _step = true;
    float _distanceToFinalTarget;

    private void Awake()
    {
        _thisCharacter = GetComponent<CharacterBase>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (_thisCharacter.CanIMove == true)
        {
            if (_thisCharacter.TargetToMoveTo != null && _thisCharacter.TargetToMoveTo.transform.position != Vector3.zero)
            {
                if (_step)
                {
                    StartCoroutine(ZWalking());
                }
                Debug.DrawLine(_thisCharacter.transform.position, _tempTargetPoint, new Color(0, 255, 0));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            print("transform.position is " + transform.position);
            print("target position is " + _thisCharacter.TargetToMoveTo.transform.position);
            print("transform.position - target position is " + (transform.position - _thisCharacter.TargetToMoveTo.transform.position));
            print("target position - transform.position is " + ((_thisCharacter.TargetToMoveTo.transform.position) - transform.position));
        }
    }

    // набор функций, где вычисл€еютс€ разные движени€? ј передавать можно делегатом кстати

    public void MoveToTarget(Vector3 target)
    {
        //_tempPoint = transform.position + (5 * (target - transform.position).normalized);
        _distanceToFinalTarget = Mathf.Abs((transform.position - target).sqrMagnitude);
    }

    IEnumerator ZWalking()
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
}