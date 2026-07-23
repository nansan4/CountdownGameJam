using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;
    [SerializeField] private List<float> dayDurationsSeconds;
    [SerializeField] private List<GameObject> circuitBoards;
    private GameObject currentBoard;
    [SerializeField] private Transform boardSpawnPos;
    [SerializeField] private List<float> dayDifficultyMultipliers;

    [Header("Round Switching")]
    [SerializeField] private Image fadeOutImage;

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

        InitializeGame();
    }

    private void InitializeGame()
    {
        if (GameState.Instance.GetGameStatus() == GameStatus.Initializing)
        {
            LeftBoxManager.Instance.SetTotalTime(dayDurationsSeconds[currentDay - 1]);
            if (currentBoard != null)
            {
                Destroy(currentBoard);
            }
            currentBoard = Instantiate(circuitBoards[currentDay-1], boardSpawnPos.position, Quaternion.identity);
            GameState.Instance.ChangeGameStatus(GameStatus.Playing);
            StartCoroutine(WaitToFadeIn());
        }
        else Debug.LogError("Game State is not in Initializing state at the start of the game.");
    }

    private void GameEnd()
    {
        currentDay++;
        StartCoroutine(FadeOut());
        StartCoroutine(WaitToSwitchScene());
    }

    private IEnumerator WaitToSwitchScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

    private void GameWin()
    {
        StartCoroutine(FadeOut());
        currentDay++;
        GameState.Instance.ChangeGameStatus(GameStatus.Initializing);
        InitializeGame();
    }

    private IEnumerator WaitToFadeIn()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeIn());
    }

    #endregion

    #region Fade In/Out

    private IEnumerator FadeIn()
    {
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color color = fadeOutImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeOutImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color color = fadeOutImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeOutImage.color = color;
            yield return null;
        }
    }

    #endregion
}
