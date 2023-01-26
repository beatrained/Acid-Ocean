using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class VisionComponent : MonoBehaviour
{
    //что тут должно быть: настраиваемый конус(с видимым гизмо в редакторе), который будет детектить нужных акторов и возвращать их

    #region gizmos
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        StaticUtils.DrawWireArc(Vector3.zero, Vector3.forward, 100, 15, 10);  // ASK ??? scr. object integration
        Gizmos.DrawWireSphere(Vector3.zero, 15);
    }
    #endregion gizmos

    [SerializeField] private LayerMask _detectionMask = ~0;

    private CharacterBase _thisCharacter;
    public CharacterBase ThisCharacter => _thisCharacter;

    private RaycastHit hitResult;

    private void Awake()
    {
        _thisCharacter = GetComponent<CharacterBase>();
    }

    private void Update()
    {
        if (DetectableTargetManager.Instance.AllTargets.Count == 0)
        {
            ThisCharacter.TargetToMoveTo = null;
            return;
        }

        for (int i = 0; i < DetectableTargetManager.Instance.AllTargets.Count; ++i)
        {
            DetectableTargetComponent potentialTarget = DetectableTargetManager.Instance.AllTargets[i];

            if (potentialTarget.gameObject == this.gameObject)
            {
                continue;
            }

            Vector3 dirToTarget = potentialTarget.transform.position - ThisCharacter.transform.position;

            if (dirToTarget.magnitude > ThisCharacter.CharacterStatsManager.AiStats.VisionRange)
            {
                continue;
            }

            if (Vector3.Dot(dirToTarget.normalized, ThisCharacter.transform.forward) < ThisCharacter.CosVisionConeAngle())
            {
                continue;
            }

            Debug.DrawLine(ThisCharacter.transform.position, potentialTarget.transform.position, Color.red);

            // TODO create somewhere a list of detected targets and deal with it
            // Also, do not raycast every frame when target is visible. Maybe another coroutine will do the trick
            if (Physics.Raycast(ThisCharacter.transform.position, dirToTarget.normalized, out hitResult,
                                ThisCharacter.CharacterStatsManager.AiStats.VisionRange, _detectionMask, QueryTriggerInteraction.Collide))
            {
                if (hitResult.collider.GetComponentInParent<DetectableTargetComponent>() == potentialTarget)
                {
                    if (ThisCharacter.TargetToMoveTo != potentialTarget.gameObject)
                    {
                        ThisCharacter.TargetToMoveTo = potentialTarget.gameObject;
                    }
                }
            }
        }
    }
}