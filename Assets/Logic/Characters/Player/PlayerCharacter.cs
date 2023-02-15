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
            ChangeState(ActorState.Wandering);
        }
    }

    private void Start()
    {
        ChangeState(ActorState.Wandering); // � playera Wandering ��� ������� ���������� �������
    }

    public override void HandleAttacking()
    {
        StartCoroutine(_charStatManagerPlayer.GetComponent<INoisy>().MakeNoise(0.5f));
        ChangeState(ActorState.Wandering);
    }

    public override void HandleWandering()
    {
        base.HandleWandering();
        CanIMove = true;
    }

    public override void HandleTakingDamage()
    {
        _charStatManagerPlayer.TakeDamage(CharacterStatsManager.IncomingDamage);
    }

    public override void HandleDying()
    {
        base.HandleDying();
        // CanIMove = false;
    }
}