using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    Tutorial
}

public class GameStateManager : MonoBehaviour
{
    public static GameState CurrentState = GameState.Playing; // Stato iniziale
}
