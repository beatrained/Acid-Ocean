using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class VisionComponent : MonoBehaviour
{
    //что тут должно быть: настраиваемый конус(с видимым гизмо в редакторе), который будет детектить нужных акторов и возвращать их

    #region gizmos
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        StaticUtils.DrawWireArc(Vector3.zero, Vector3.forward, 100, 15, 10);  // TODO ??? scr. object integration
        Gizmos.DrawWireSphere(Vector3.zero, 15);
    }
    #endregion gizmos

    [SerializeField] private LayerMask _detectionMask = ~0;

    public CharacterBase _thisCharacter { get; private set; }

    private RaycastHit hitResult;

    private void Start()
    {
        _thisCharacter = GetComponent<EnemyCharacter>() as EnemyFlowerBot;
    }

    private void Update()
    {
        if (DetectableTargetManager.Instance.AllTargets.Count == 0)
        {
            _thisCharacter.TargetToMoveTo = null;
            return;
        }

        for (int i = 0; i < DetectableTargetManager.Instance.AllTargets.Count; ++i)
        {
            DetectableTargetComponent potentialTarget = DetectableTargetManager.Instance.AllTargets[i];

            if (potentialTarget.gameObject == this.gameObject)
            {
                continue;
            }

            Vector3 dirToTarget = potentialTarget.transform.position - _thisCharacter.transform.position;

            if (dirToTarget.magnitude > _thisCharacter.CharStats.VisionRange)
            {
                continue;
            }

            if (Vector3.Dot(dirToTarget.normalized, _thisCharacter.transform.forward) < _thisCharacter.CosVisionConeAngle())
            {
                continue;
            }

            Debug.DrawLine(_thisCharacter.transform.position, potentialTarget.transform.position, Color.red);

            // TODO create somewhere a list of detected targets and deal with it
            // Also, do not raycast every frame when target is visible. Maybe another coroutine will do the trick
            if (Physics.Raycast(_thisCharacter.transform.position, dirToTarget.normalized, out hitResult,
                                _thisCharacter.CharStats.VisionRange, _detectionMask, QueryTriggerInteraction.Collide))
            {
                if (hitResult.collider.GetComponentInParent<DetectableTargetComponent>() == potentialTarget)
                {
                    if (_thisCharacter.TargetToMoveTo != potentialTarget.gameObject)
                    {
                        _thisCharacter.TargetToMoveTo = potentialTarget.gameObject;
                        //_thisCharacter.ChangeState(ActorState.AccuireTarget);         // ChangeState must be called only inside actor class!!!!! done
                    }
                }
            }
        }
    }
}