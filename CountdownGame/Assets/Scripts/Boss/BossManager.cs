using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region Boss Settings
    [Header("Boss Settings")]
    public float difficultyMultiplier;
    [Header("Mess With Player Settings")]
    [SerializeField] private float messWithPlayerInterval = 30f;
    [SerializeField] private float messWithPlayerChance = 0.5f;
    [SerializeField] private float messWithPlayerTimer = 0f;
    public int optionsCount; //1 is toolbar, 2 is that plus magnifying glass, 3 is both plus 5th tool
    [Header("Player Affected Settings")]
    [SerializeField] private float mouseDownInterval = 0.5f;
    [SerializeField] private float mouseDownTimer = 0f;
    [SerializeField] private float playerAffectedChance = 0.5f;
    [Header("Random Comment Settings")]
    [SerializeField] private float randomCommentInterval = 10f;
    [SerializeField] private float randomCommentChance = 0.5f;
    [SerializeField] private float randomCommentTimer = 0f;
    #endregion
    #region Boss Audio
    [Header("Boss Audio")]
    [SerializeField] private AudioSource bossAudioSource;
    [Header("Liam Clips")]
    [SerializeField] private List<BossClipSO> LiamMessWithPlayerClips;
    [SerializeField] private List<BossClipSO> LiamPlayerAffectedClips;
    [SerializeField] private List<BossClipSO> LiamRandomCommentClips;
    [Header("Nando Clips")]
    [SerializeField] private List<BossClipSO> NandoMessWithPlayerClips;
    [SerializeField] private List<BossClipSO> NandoPlayerAffectedClips;
    [SerializeField] private List<BossClipSO> NandoRandomCommentClips;
    [Header("Tim Clips")]
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
        if (GameState.Instance.GetGameStatus() == GameStatus.Playing)
        {
            // HandleMessWithPlayer();
            // HandleRandomComment();
        }
    }

    #endregion
}
