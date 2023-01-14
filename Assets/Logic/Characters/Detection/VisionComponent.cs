using UnityEngine;

[RequireComponent(typeof(EnemyCharacter))]
public class VisionComponent : MonoBehaviour
{
    //что тут должно быть: настраиваемый конус(с видимым гизмо в редакторе), который будет детектить нужных акторов и возвращать их

    #region gizmos

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        StaticUtils.DrawWireArc(Vector3.zero, Vector3.forward, 100, 15, 10);  // TODO scr. object integration
        Gizmos.DrawWireSphere(Vector3.zero, 10);
    }

    #endregion gizmos

    [SerializeField] private LayerMask _detectionMask = ~0;

    public EnemyFlowerBot _thisEnemyCharacter { get; private set; }

    private RaycastHit hitResult;

    private void Start()
    {
        _thisEnemyCharacter = GetComponent<EnemyCharacter>() as EnemyFlowerBot;
    }

    private void Update()
    {
        for (int i = 0; i < DetectableTargetManager.Instance.AllTargets.Count; ++i)
        {
            DetectableTargetComponent potentialTarget = DetectableTargetManager.Instance.AllTargets[i];

            if (potentialTarget.gameObject == this.gameObject)
            {
                continue;
            }

            Vector3 dirToTarget = potentialTarget.transform.position - _thisEnemyCharacter.transform.position;

            if (dirToTarget.magnitude > _thisEnemyCharacter.CharStats.VisionRange)
            {
                continue;
            }

            if (Vector3.Dot(dirToTarget.normalized, _thisEnemyCharacter.transform.forward) < _thisEnemyCharacter.CosVisionConeAngle())
            {
                continue;
            }

            Debug.DrawLine(_thisEnemyCharacter.transform.position, potentialTarget.transform.position, Color.red);

            if (Physics.Raycast(_thisEnemyCharacter.transform.position, dirToTarget.normalized, out hitResult,
                                _thisEnemyCharacter.CharStats.VisionRange, _detectionMask, QueryTriggerInteraction.Collide))
            {
                if (hitResult.collider.GetComponentInParent<DetectableTargetComponent>() == potentialTarget)
                {
                    _thisEnemyCharacter.CanSeeTarget(potentialTarget);
                }
            }
        }
    }
}