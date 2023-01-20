using UnityEngine;
using AcidOcean.Game;

public class PlayerCharacter : CharacterBase
{
    private void OnDestroy() => GameProcessManager.OnBeforeStateChanged -= OnStateChanged;

    private CharStatsManagerPlayer _charStatManagerPlayer;
    public CharStatsManagerPlayer ChaStatManagerPlayer => _charStatManagerPlayer;

    CharStatsManagerPlayer _characterStatsManagerPlayer;

    private bool _isSpeedMultiplied = false;
    private float _speedStorage;

    private void Awake()
    { 
        RunOnAwake();
        GameProcessManager.OnBeforeStateChanged += OnStateChanged;
        _characterStatsManagerPlayer = GetComponent<CharStatsManagerPlayer>();
    }
    public bool IsSpeedMultiplied
    {
        get
        {
            return _isSpeedMultiplied;
        }
        set
        {
            if (value == true)
            {
                _speedStorage = CharacterStatsManager.CharBasicStats.Speed;
                CharacterStatsManager.CharBasicStats.Speed = _characterStatsManagerPlayer.SpeedOnFourLegs;
                
            } else
            {
                CharacterStatsManager.CharBasicStats.Speed = _speedStorage;
            }
            _isSpeedMultiplied = value;
        }
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            CanIMove = true;
        }
    }

    private void Start()
    {
        ChangeState(ActorState.Wandering); // у playera Wandering это обычное управление игроком
    }
    private void OnEnable()
    {
        LocalEventManager.StandOnTwoLegs += EventManager_StandOnTwoLegs;
        LocalEventManager.StandOnFourLegs += EventManager_StandOnFourLegs;
    }

    private void EventManager_StandOnFourLegs()
    {
        IsSpeedMultiplied = false;
    }

    private void EventManager_StandOnTwoLegs()
    {
        IsSpeedMultiplied = true;
    }

    public override void HandleTakingDamage()
    {
        _characterStatsManagerPlayer.TakeDamage(CharacterStatsManager.IncomingDamage);
    }

    public override void HandleDying()
    {
        base.HandleDying();
        CanIMove = false;                       //AI only
    }
}