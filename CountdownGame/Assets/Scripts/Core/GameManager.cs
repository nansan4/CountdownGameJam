using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;
    [SerializeField] private List<float> dayDurationsSeconds;
    [SerializeField] private List<float> dayDifficultyMultipliers;

    #region Singleton

    public static GameManager Instance { get; private set; }
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

    #region Unity Lifecycle

    void Start()
    {
        GameState.Instance.GameOverEvent.AddListener(GameEnd);
        GameState.Instance.VictoryEvent.AddListener(GameWin);

        if (GameState.Instance.GetGameStatus() == GameStatus.Initializing)
        {
            LeftBoxManager.Instance.SetTotalTime(dayDurationsSeconds[currentDay - 1]);
            GameState.Instance.ChangeGameStatus(GameStatus.Playing);
        }
        else Debug.LogError("Game State is not in Initializing state at the start of the game.");
    }

    private void GameEnd()
    {
        currentDay++;
    }

    private void GameWin()
    {
        currentDay++;
    }

    #endregion
}
