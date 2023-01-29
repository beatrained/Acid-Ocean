using UnityEngine;
using AcidOcean.Game;

public class PlayerCharacter : CharacterBase
{
    private void OnDestroy() => GameProcessManager.OnBeforeStateChanged -= OnStateChanged;

    private CharStatsManagerPlayer _charStatManagerPlayer;
    public CharStatsManagerPlayer ChaStatManagerPlayer => _charStatManagerPlayer;

    //CharStatsManagerPlayer _characterStatsManagerPlayer;

    private void Awake()
    { 
        RunOnAwake();
        GameProcessManager.OnBeforeStateChanged += OnStateChanged;
        _charStatManagerPlayer = GetComponent<CharStatsManagerPlayer>();
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

    public override void HandleTakingDamage()
    {
        _charStatManagerPlayer.TakeDamage(CharacterStatsManager.IncomingDamage);
    }

    public override void HandleDying()
    {
        base.HandleDying();
        CanIMove = false;                       //AI only
    }
}