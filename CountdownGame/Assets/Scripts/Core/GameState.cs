using UnityEngine;
using UnityEngine.Events;

public enum GameStatus
{
    Initializing,
    Playing,
    Paused,
    Tutorial,
    GameOver,
    Victory
}

public class GameState : MonoBehaviour
{
    [SerializeField] private GameStatus currentGameStatus = GameStatus.Initializing;
    public UnityEvent VictoryEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent PauseEvent;

    #region Singleton
    public static GameState Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    #region Game Status Management

    public GameStatus GetGameStatus()
    {
        return currentGameStatus;
    }

    public void ChangeGameStatus(GameStatus newStatus)
    {
        HandleGameStatusChange(newStatus);
    }

    private void HandleGameStatusChange(GameStatus newStatus)
    {
        switch (newStatus)
        {
            case GameStatus.Initializing:
                // Handle initializing logic
                currentGameStatus = newStatus;
                break;
            case GameStatus.Playing:
                // Handle playing logic
                currentGameStatus = newStatus;
                break;
            case GameStatus.Paused:
                // Handle paused logic
                currentGameStatus = newStatus;
                PauseEvent?.Invoke();
                break;
            case GameStatus.Tutorial:
                // Handle tutorial logic
                currentGameStatus = newStatus;
                break;
            case GameStatus.GameOver:
                // Handle game over logic
                currentGameStatus = newStatus;
                GameOverEvent?.Invoke();
                Debug.Log("Day lost!");
                break;
            case GameStatus.Victory:
                // Handle victory logic
                currentGameStatus = newStatus;
                VictoryEvent?.Invoke();
                Debug.Log("Day won!");
                break;
            default:
                Debug.LogWarning("Unhandled game status: " + newStatus);
                break;
        }
    }

    #endregion
}
