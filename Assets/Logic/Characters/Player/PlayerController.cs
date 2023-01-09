using UnityEngine;
using UnityEngine.InputSystem;
using AcidOcean.Game;


namespace AcidOcean.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private PlayerInputActions _playerInputActions;
        private PlayerCharacter _playerCharacter;
        private Animator _animator;

        private Vector2 _inputVector;
        private Vector3 _playerLookDir;
        private Vector3 _force;
        [SerializeField] private float _playerSpeed = 10f;
        [SerializeField][Range(0, 1)] private float _playerRotationSpeed = 0.1f;
        private bool _disableControls = false;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _playerCharacter = GetComponent<PlayerCharacter>();

            _playerInputActions = new PlayerInputActions();
            _playerInputActions.PlayerBasicMovement.Enable();
            _playerInputActions.PlayerBasicMovement.Jump.performed += Jump_performed;
            _playerInputActions.PlayerBasicMovement.ChangePose.performed += ChangePose_performed;
        }

        void UpdateStats()
        {
            _playerSpeed = _playerCharacter.Stats.Speed;
        }


        private void FixedUpdate()
        {
            _inputVector = _playerInputActions.PlayerBasicMovement.Movement.ReadValue<Vector2>();
            _force = _playerLookDir * _playerSpeed;
            _rigidbody.AddForce(_force, ForceMode.Force);
            _animator.SetFloat("Speed", _force.magnitude);
        }

        private void Update()
        {
            _playerLookDir = new Vector3(_inputVector.x, 0, _inputVector.y);

            if (_playerLookDir != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(_playerLookDir, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _playerRotationSpeed);
            }

            if (_disableControls)
            {
                _playerInputActions.PlayerBasicMovement.Disable();
            } else
            {
                _playerInputActions.PlayerBasicMovement.Enable();
            }
        }

        private void ChangePose_performed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                UpdateStats();
                _animator.StopPlayback();
                if (_animator.GetBool("OnTwoLegs") == false)
                {
                    LocalEventManager.StayOnTwo();
                    _animator.SetBool("OnTwoLegs", true);
                }
                else
                {
                    LocalEventManager.StayOnFour();
                    _animator.SetBool("OnTwoLegs", false);
                }
            }
        }

        // Animation Event on player character StandUp animation clip
        public void FreezePlayer()
        {
            _disableControls = !_disableControls;
        }

        private void Jump_performed(InputAction.CallbackContext context)
        {
            UpdateStats();
            if (context.performed)
            {
                print("jumping..?");
            }
        }
    }
}