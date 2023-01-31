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
        private CharStatsManagerPlayer _charStatsManagerPlayer;
        private Animator _animator;

        private Vector2 _inputVector;
        private Vector3 _playerLookDir;
        private Vector3 _force;
        private float _currentSpeed;
        
        [SerializeField][Range(0, 1)] private float _playerRotationSpeed = 0.1f;

        public float PlayerSpeed 
        { 
            get
            {
                if (_animator.GetBool("OnTwoLegs"))
                {
                    return _charStatsManagerPlayer.CharBasicStats.Speed;
                }
                else
                {
                    return _charStatsManagerPlayer.SpeedOnFourLegs;
                }
            } 
            set { _charStatsManagerPlayer.CharBasicStats.Speed = value; }
        }

        [SerializeField] private GameObject _axeInHands;        // TODO change to static links
        [SerializeField] private GameObject _turretOnTheBack;
        [SerializeField] private Animator _axeAnimator;

        private BigBotAxe _bigBotAxe;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _playerCharacter = GetComponent<PlayerCharacter>();
            _charStatsManagerPlayer = GetComponent<CharStatsManagerPlayer>();

            _playerInputActions = new PlayerInputActions();
            _playerInputActions.PlayerBasicMovement.Enable();
            _playerInputActions.PlayerBasicMovement.ChangePose.performed += ChangePose_performed;
            _playerInputActions.PlayerBasicMovement.Attack.performed += Attack_performed;
            _playerInputActions.PlayerBasicMovement.Block.started += Block_started;
            _playerInputActions.PlayerBasicMovement.Block.canceled += Block_canceled;

            _currentSpeed = PlayerSpeed;
            _bigBotAxe = _axeInHands.GetComponent<BigBotAxe>();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void Update()
        {
            _playerLookDir = new Vector3(_inputVector.x, 0, _inputVector.y);

            if (_playerLookDir != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(_playerLookDir, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _playerRotationSpeed);
            }
        }

        private void MoveCharacter()
        {
            if (_playerCharacter.CanIMove)
            {
                _inputVector = _playerInputActions.PlayerBasicMovement.Movement.ReadValue<Vector2>();
                _force = _playerLookDir * _currentSpeed;
                _rigidbody.AddForce(_force, ForceMode.Force);
                _animator.SetFloat("Speed", _force.magnitude);   // TODO get rid of .magnitude?
            } else
            {
                _animator.SetFloat(name: "Speed", 0);
            }
        }

        void UpdateStats()
        {
            _currentSpeed = PlayerSpeed;
        }

        private void Attack_performed(InputAction.CallbackContext context)
        {
            if (_animator.GetBool("OnTwoLegs"))
            {
                _animator.SetTrigger("AttackTrigger");    // mmmmmmm we definetely need script on axe itself, because of the collider on off
            }
        }

        private void Block_started(InputAction.CallbackContext obj)
        {
            _animator.SetBool("Block", true);
            _bigBotAxe.IsShieldEnabled = true;
        }

        private void Block_canceled(InputAction.CallbackContext obj)
        {
            _animator.SetBool("Block", false);
            _bigBotAxe.IsShieldEnabled = false;
        }

        private void ChangePose_performed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (_animator.GetBool("OnTwoLegs") == false)
                {
                    _animator.SetBool("OnTwoLegs", true);
                }
                else
                {
                    _animator.SetBool("OnTwoLegs", false);
                }
                UpdateStats();
            }
        }

        // Animation Event on player character StandUp and WeaponAttackONE-mod1 animation clips
        public void _FreezePlayer()
        {
            _playerCharacter.CanIMove = !_playerCharacter.CanIMove;
        }

        // Animation Event on player character WeaponEquipX-mod1 animation clip
        public void _ShowAxeInHands()
        {
            if (!_axeInHands.activeSelf)
            {
                _axeInHands.SetActive(true);
                _turretOnTheBack.SetActive(false);
            }
            else
            {
                _axeInHands.SetActive(false);
                _turretOnTheBack.SetActive(true);
            }
        }

        // Animation Event on player character WeaponEquipX-mod1 animation clip
        public void _FoldAxe()
        {
            if (_animator.GetBool("OnTwoLegs"))
            {
                _axeAnimator.SetBool("Transformed", true);
            }
            else
            {
                _axeAnimator.SetBool("Transformed", false);
            }
        }

        // Anim event on attackTWO animation
        public void _EnableAxeCollision()
        {
            _bigBotAxe.IsColliderEnabled = !_bigBotAxe.IsColliderEnabled;
        }
    }
}