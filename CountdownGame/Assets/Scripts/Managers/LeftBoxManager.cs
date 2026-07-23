using UnityEngine;
using TMPro;
using System.Collections;

public class LeftBoxManager : MonoBehaviour
{
   [SerializeField] private TMP_Text clockText;
   [SerializeField] private TMP_Text dialogueText;
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
        int totalSeconds = Mathf.Max(0, Mathf.FloorToInt(remainingGameTime));
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        clockText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
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
