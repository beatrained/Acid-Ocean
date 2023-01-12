using UnityEngine;
using AcidOcean.Game;

public class PlayerCharacter : CharacterBase
{
    private void Awake() => GameProcessManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameProcessManager.OnBeforeStateChanged -= OnStateChanged;

    [SerializeField] private ScrPlayerCharacter _scrPlayerCharacter;

    private bool _isSpeedMultiplied = false;

    BasicActorStats PlayerStats = new BasicActorStats(); //
    private float _speedStorage;

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
                _speedStorage = PlayerStats.Speed;  //
                PlayerStats.Speed = _scrPlayerCharacter.SpeedOnFourLegs; //         CharStats.Speed * CharStats.SpeedMlt;
                
            } else
            {
                PlayerStats.Speed = _speedStorage;
            }
            SetStats(PlayerStats);
            _isSpeedMultiplied = value;
        }
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            CanMove = true;
        }
    }

    private void Start()
    {
        SetStats(_scrPlayerCharacter.BaseStats);    // -> to loading state?
        PlayerStats = CharStats; //
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
}