using AcidOcean.Game;
using AcidOcean.Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class CannonController : MonoBehaviour
{
    public bool CanTurretRotate { get; set; } = true;

    private PlayerInputActions _playerInputActions;
    private Vector3 _pointToLookAt;
    private Vector2 _mouseCoords;
    private Ray _ray;
    private Quaternion _previousRotation;
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private Camera _camera;
    [SerializeField] GameObject _objectToRotate;
    [SerializeField] LayerMask _layerMask;

    private void Awake()
    {
        LocalEventManager.StandOnFourLegs += LocalEventManager_StandOnFourLegs;
        LocalEventManager.StandOnTwoLegs += LocalEventManager_StandOnTwoLegs;
        _previousRotation = _objectToRotate.transform.rotation;
    }

    private void Start()
    {
        _playerInputActions = GetComponent<PlayerController>().PlayerInputActions;
    }

    // X & Z must be 0

    private void LocalEventManager_StandOnTwoLegs()
    {
        //_previousRotation = _objectToRotate.transform.rotation;
        Invoke("TurretActivation", 1.5f);
    }

    private void LocalEventManager_StandOnFourLegs()
    {
        //_objectToRotate.transform.rotation = _previousRotation;
        //_objectToRotate.transform.localRotation = GetComponentInParent<Transform>().rotation;
        TurretActivation();
    }

    private void TurretActivation()
    {
        CanTurretRotate = !CanTurretRotate;
    }

    private void Update()
    {
        if (CanTurretRotate)
        {
            RotateTurret();
        }
    }

    private void RotateTurret()
    {
        _mouseCoords = _playerInputActions.PlayerBasicMovement.MousePosition.ReadValue<Vector2>();
        _ray = _camera.ScreenPointToRay(_mouseCoords);

        if (Time.frameCount % 2 == 0)
        {
            Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, _layerMask);
            _pointToLookAt = hit.point;
        }

        Vector3 direction = _pointToLookAt - _objectToRotate.transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(direction);
        Quaternion.FromToRotation(_objectToRotate.transform.forward, direction);
        Quaternion lookAtRotationOnlyY = Quaternion
            .Euler(_objectToRotate.transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, _objectToRotate.transform.rotation.eulerAngles.z);
        _objectToRotate.transform.rotation = Quaternion.Lerp(_objectToRotate.transform.rotation, lookAtRotationOnlyY, _rotationSpeed * Time.deltaTime);
    }
}
