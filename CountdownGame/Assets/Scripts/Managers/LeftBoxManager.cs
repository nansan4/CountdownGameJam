using UnityEngine;
using TMPro;
using System.Collections;

public class LeftBoxManager : MonoBehaviour
{
   [SerializeField] private TMP_Text clockText;
   [SerializeField] private TMP_Text dialogueText;
   [SerializeField] private AudioSource clockTickingAudioSource;
   [SerializeField] private AudioClip minuteTickSound;
   [SerializeField] private AudioClip countdownTickSound;
   private int lastCountdownSecond = -1;
   [SerializeField] private float totalGameTime;
   [SerializeField] private float remainingGameTime;

   #region Singleton
   public static LeftBoxManager Instance { get; private set; }
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

    public void SetTotalTime(float time)
    {
        totalGameTime = time;
        remainingGameTime = totalGameTime;
    }

    void FixedUpdate()
    {
        if (GameState.Instance.GetGameStatus() == GameStatus.Playing)
        {
            remainingGameTime -= Time.fixedDeltaTime;
            UpdateClockText();
        }
    }

    private void UpdateClockText()
    {
        int totalMilliseconds = Mathf.Max(0, Mathf.FloorToInt(remainingGameTime * 1000f));
        int minutes = totalMilliseconds / 60000;
        int seconds = totalMilliseconds % 60000 / 1000;
        int milliseconds = totalMilliseconds % 1000;

        if (seconds == 0)
        {
            clockTickingAudioSource.PlayOneShot(minuteTickSound);
        }

        // Play a tick once per second during the final 30 seconds
        int wholeSecondsRemaining = Mathf.CeilToInt(remainingGameTime);

        if (wholeSecondsRemaining <= 30 &&
            wholeSecondsRemaining > 0 &&
            wholeSecondsRemaining != lastCountdownSecond)
        {
            clockTickingAudioSource.PlayOneShot(countdownTickSound);
            lastCountdownSecond = wholeSecondsRemaining;
        }

        clockText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    #endregion


    #region Text Box Handling

    private Coroutine typingCoroutine;

    public void ShowText(string message, float charactersPerSecond = 30f)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message, charactersPerSecond));
    }

    private IEnumerator TypeText(string message, float charactersPerSecond)
    {
        dialogueText.text = message;
        dialogueText.maxVisibleCharacters = 0;

        dialogueText.ForceMeshUpdate();

        int totalCharacters = dialogueText.textInfo.characterCount;

        for (int i = 0; i <= totalCharacters; i++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(1f / charactersPerSecond);
        }
    }

    #endregion
}
