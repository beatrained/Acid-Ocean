using UnityEngine;
using UnityEngine.InputSystem;
using AcidOcean.Game;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;

namespace AcidOcean.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public PlayerInputActions PlayerInputActions { get; set; }
        private PlayerCharacter _playerCharacter;
        private CharStatsManagerPlayer _charStatsManagerPlayer;
        private Animator _animator;

        private Vector2 _inputVector;
        private Vector3 _playerLookDir;
        private Vector3 _force;
        private float _currentSpeed;
        
        [SerializeField][Range(0, 1)] private float _playerRotationSpeed = 0.1f;

        [SerializeField] private GameObject _axeInHands;        // TODO change to static links
        [SerializeField] private GameObject _turretOnTheBack;
        [SerializeField] private Animator _axeAnimator;

        

        private BigBotAxe _bigBotAxe;

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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _playerCharacter = GetComponent<PlayerCharacter>();
            _charStatsManagerPlayer = GetComponent<CharStatsManagerPlayer>();

            PlayerInputActions = new PlayerInputActions();
            PlayerInputActions.PlayerBasicMovement.Enable();
            PlayerInputActions.PlayerBasicMovement.ChangePose.performed += ChangePose_performed;
            PlayerInputActions.PlayerBasicMovement.Attack.performed += Attack_performed;
            PlayerInputActions.PlayerBasicMovement.Block.started += Block_started;
            PlayerInputActions.PlayerBasicMovement.Block.canceled += Block_canceled;

            //_currentSpeed = PlayerSpeed;
            _bigBotAxe = _axeInHands.GetComponent<BigBotAxe>();
        }

        private void Start()
        {
            _currentSpeed = PlayerSpeed;
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void Update()
        {
            RotateCharacter();
            //Debug.Log("Player speed is equals " + PlayerSpeed);
        }

        private void RotateCharacter()
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
                _inputVector = PlayerInputActions.PlayerBasicMovement.Movement.ReadValue<Vector2>();
                _force = _playerLookDir * _currentSpeed;
                _rigidbody.AddForce(_force, ForceMode.Force);
                _animator.SetFloat("Speed", _force.magnitude);   // TODO get rid of .magnitude?
            } else
            {
                _animator.SetFloat(name: "Speed", 0);
            }
        }
        private void OnCollisionEnter(Collision col)
        {
            IDamaging idam = col.gameObject.GetComponent<IDamaging>();
            if (idam == null || col.gameObject.layer != 11 || _animator.GetBool("Block")) return;
            GlobalEventManager.PlayerDamaged(idam.DamageAmount);
        }

        void UpdateStats()
        {
            _currentSpeed = PlayerSpeed;
        }

        private void Attack_performed(InputAction.CallbackContext context)
        {
            if (_animator.GetBool("OnTwoLegs"))
            {
                _animator.SetTrigger("AttackTrigger");
                StartCoroutine(FreezePlayerDuringAnimation(1, 0.3f));
                _playerCharacter.AttackPlease();
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
                    LocalEventManager.StayOnFour();
                    _animator.SetBool("OnTwoLegs", true);
                    StartCoroutine(FreezePlayerDuringAnimation(0, 0.3f));
                }
                else
                {
                    LocalEventManager.StayOnTwo();
                    _animator.SetBool("OnTwoLegs", false);
                    StartCoroutine(FreezePlayerDuringAnimation(0, 0.3f));
                }
                UpdateStats();
            }
        }

        public IEnumerator FreezePlayerDuringAnimation(int layerIndex, float timeCorrection)
        {
            // for 100% correct GetCurrentAnimatorStateInfo pick 
            yield return new WaitForSeconds(0.3f);

            float time = _animator.GetCurrentAnimatorStateInfo(layerIndex).length;
            _playerCharacter.CanIMove = false;
            yield return new WaitForSeconds(time - timeCorrection);
            _playerCharacter.CanIMove = true;
        }

        // Animation Event on player character WeaponEquipX-mod1 animation clip
        public void _ShowAxeInHands()
        {
            if (!_axeInHands.activeSelf && _animator.GetBool("OnTwoLegs"))
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