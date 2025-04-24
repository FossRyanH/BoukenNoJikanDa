using Godot;
using System;
using System.Collections.Generic;

public partial class GameStateManager : Singleton<GameStateManager>
{
    public static GameState GameState { get; set; }
    public static event Action<GameState> GameStateChanged;

    protected override void Initialize()
    {
        HandleGameStateChange(GameState.GamePlay);
    }

    public void HandleGameStateChange(GameState state) => GameStateChanged?.Invoke(state);
}

public enum GameState { GamePlay, CinematicEvent, Dialogue, Menus }
