using UnityEngine;
using AcidOcean.Game;

public class PlayerCharacter : CharacterBase
{
    private void Awake() => GameProcessManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameProcessManager.OnBeforeStateChanged -= OnStateChanged;

    [SerializeField] private ScrPlayerCharacter _scrPlayerCharacter;

    private bool _isSpeedMultiplied = false;

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
                SetStats(new Stats(Stats.Health, Stats.Speed * _scrPlayerCharacter.BaseStats.SpeedMlt, Stats.SpeedMlt));
                print(Stats.Speed);
            } else
            {
                SetStats(new Stats(Stats.Health, _scrPlayerCharacter.BaseStats.Speed, Stats.SpeedMlt));
                print(Stats.Speed);
            }
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