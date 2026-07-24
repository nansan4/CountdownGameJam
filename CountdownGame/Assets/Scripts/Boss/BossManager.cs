using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region Boss Settings
    [Header("Boss Settings")]
    public float difficultyMultiplier;
    [SerializeField] private float switchBossInterval = 15f;
    [SerializeField] private float switchBossChance = 0.5f;
    [SerializeField] private float switchBossTimer = 0f;
    [SerializeField] private int currentBossIndex = 0; //0 for Liam, 1 for Nando, 2 for Tim
    [Header("Mess With Player Settings")]
    [SerializeField] private float messWithPlayerInterval = 30f;
    [SerializeField] private float messWithPlayerChance = 0.5f;
    [SerializeField] private float messWithPlayerTimer = 0f;
    public int optionsCount; //1 is toolbar, 2 is that plus magnifying glass, 3 is both plus 5th tool
    [Header("Player Affected Settings")]
    [SerializeField] private float mouseDownInterval = 0.5f;
    [SerializeField] private float mouseDownTimer = 0f;
    [SerializeField] private float playerAffectedChance = 0.5f;
    [Header("Mouse Down Settings")]
    private bool mouseDown = false;
    [Header("Random Comment Settings")]
    [SerializeField] private float randomCommentInterval = 10f;
    [SerializeField] private float randomCommentChance = 0.5f;
    [SerializeField] private float randomCommentTimer = 0f;
    #endregion
    #region Boss Audio
    [Header("Boss Audio")]
    [SerializeField] private AudioSource bossAudioSource;
    [Header("Liam Events")]
    [SerializeField] private List<BossClipSO> LiamMessWithPlayerClips;
    [SerializeField] private List<BossClipSO> LiamPlayerAffectedClips;
    [SerializeField] private List<BossClipSO> LiamRandomCommentClips;
    [Header("Nando Events")]
    [SerializeField] private List<BossClipSO> NandoMessWithPlayerClips;
    [SerializeField] private List<BossClipSO> NandoPlayerAffectedClips;
    [SerializeField] private List<BossClipSO> NandoRandomCommentClips;
    [Header("Tim Events")]
    [SerializeField] private List<BossClipSO> TimMessWithPlayerClips;
    [SerializeField] private List<BossClipSO> TimPlayerAffectedClips;
    [SerializeField] private List<BossClipSO> TimRandomCommentClips;
    #endregion


    #region Singleton
    public static BossManager Instance { get; private set; }
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

    void Update()
    {
        // Update timers
        switchBossTimer += Time.deltaTime;
        messWithPlayerTimer += Time.deltaTime;
        if (mouseDown)
        {
            mouseDownTimer += Time.deltaTime;
        }
        else
        {
            mouseDownTimer = 0f; // Reset the timer if the mouse is not down
        }
        randomCommentTimer += Time.deltaTime;

        // Check timers
        if (switchBossTimer >= switchBossInterval)
        {
            TrySwitchBoss();
            switchBossTimer = 0f;
        }
        if (messWithPlayerTimer >= messWithPlayerInterval)
        {
            TryMessWithPlayer();
            messWithPlayerTimer = 0f;
        }
        if (randomCommentTimer >= randomCommentInterval)
        {
            TryRandomComment();
            randomCommentTimer = 0f;
        }
    }

    private void PlayBossClip(BossClipSO clipSO)
    {
        if (clipSO.clip != null)
        {
            bossAudioSource.clip = clipSO.clip;
            bossAudioSource.Play();
            Debug.Log($"Playing Boss Clip: {clipSO.text}");
        }
        else
        {
            Debug.LogWarning("BossClipSO has no AudioClip assigned.");
        }

        // Trigger the associated event if it exists
        if (clipSO.eventToTrigger != null)
        {
            clipSO.eventToTrigger.Execute(this);
        }
    }

    #endregion

    #region Boss Actions

    void TrySwitchBoss()
    {
        if (Random.value < switchBossChance)
        {
            // Logic to switch boss
            currentBossIndex = (currentBossIndex + 1) % 3; // Cycle through 0, 1, 2
            Debug.Log("Switching Boss to index: " + currentBossIndex);
        }
    }

    void TryMessWithPlayer()
    {
        if (Random.value < messWithPlayerChance)
        {
            // Logic to mess with player
            switch (currentBossIndex)
            {
                case 0: // Liam
                    if (LiamMessWithPlayerClips.Count > 0)
                    {
                        var clipSO = LiamMessWithPlayerClips[Random.Range(0, LiamMessWithPlayerClips.Count)];
                        PlayBossClip(clipSO);
                        clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                    }
                    break;
                case 1: // Nando
                    if (NandoMessWithPlayerClips.Count > 0)
                    {
                        var clipSO = NandoMessWithPlayerClips[Random.Range(0, NandoMessWithPlayerClips.Count)];
                        PlayBossClip(clipSO);
                        clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                    }
                    break;
                case 2: // Tim
                    if (TimMessWithPlayerClips.Count > 0)
                    {
                        var clipSO = TimMessWithPlayerClips[Random.Range(0, TimMessWithPlayerClips.Count)];
                        PlayBossClip(clipSO);
                        clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                    }
                    break;
            }
            Debug.Log("Messing with Player!");
        }
    }

    private void OnMouseDown()
    {
        mouseDown = true;
        if (mouseDownTimer >= mouseDownInterval)
        {
            if (Random.value < playerAffectedChance)
            {
                // Logic to affect the player
                switch (currentBossIndex)
                {
                    case 0: // Liam
                        if (LiamPlayerAffectedClips.Count > 0)
                        {
                            var clipSO = LiamPlayerAffectedClips[Random.Range(0, LiamPlayerAffectedClips.Count)];
                            PlayBossClip(clipSO);
                            clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                        }
                        break;
                    case 1: // Nando
                        if (NandoPlayerAffectedClips.Count > 0)
                        {
                            var clipSO = NandoPlayerAffectedClips[Random.Range(0, NandoPlayerAffectedClips.Count)];
                            PlayBossClip(clipSO);
                            clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                        }
                        break;
                    case 2: // Tim
                        if (TimPlayerAffectedClips.Count > 0)
                        {
                            var clipSO = TimPlayerAffectedClips[Random.Range(0, TimPlayerAffectedClips.Count)];
                            PlayBossClip(clipSO);
                            clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                        }
                        break;
                }
                Debug.Log("Affecting Player!");
            }
            mouseDownTimer = 0f;
        }
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    void TryRandomComment()
    {
        if (Random.value < randomCommentChance)
        {
            // Logic to make a random comment
            switch (currentBossIndex)
            {
                case 0: // Liam
                    if (LiamRandomCommentClips.Count > 0)
                    {
                        var clipSO = LiamRandomCommentClips[Random.Range(0, LiamRandomCommentClips.Count)];
                        PlayBossClip(clipSO);
                        clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                    }
                    break;
                case 1: // Nando
                    if (NandoRandomCommentClips.Count > 0)
                    {
                        var clipSO = NandoRandomCommentClips[Random.Range(0, NandoRandomCommentClips.Count)];
                        PlayBossClip(clipSO);
                        clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                    }
                    break;
                case 2: // Tim
                    if (TimRandomCommentClips.Count > 0)
                    {
                        var clipSO = TimRandomCommentClips[Random.Range(0, TimRandomCommentClips.Count)];
                        PlayBossClip(clipSO);
                        clipSO.eventToTrigger?.Execute(this); // Trigger the event if it exists
                    }
                    break;
            }
            Debug.Log("Making a Random Comment!");
        }
    }

    #endregion
}
