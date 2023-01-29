using System;
using UnityEngine;

public class GameProcessManager : MonoBehaviour
{
    public static event Action<GameState> OnBeforeStateChanged; // ивенты при смене стейта

    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    private void Start()
    {
        ChangeState(GameState.Running); // temp for now
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;

            case GameState.ResourceLoading:
                break;

            case GameState.Running:
                HandleRunning();
                break;

            case GameState.PauseMenu:
                HandlePauseMenu();
                break;

            case GameState.InformationScreen:
                break;

            case GameState.GameOverScreen:
                break;

            default:
                break;
        }

        Debug.Log($"New Game State: {newState}");
    }

    private void HandlePauseMenu()
    {
    }

    private void HandleRunning()
    {
    }
}

[Serializable]
public enum GameState
{
    MainMenu,
    ResourceLoading,
    Running,
    PauseMenu,
    InformationScreen,
    GameOverScreen,
}