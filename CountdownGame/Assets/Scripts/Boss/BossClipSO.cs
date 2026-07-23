using UnityEngine;
[CreateAssetMenu(fileName = "New Boss Clip", menuName = "Boss Clip", order = 1)]
public class BossClipSO : ScriptableObject
{
    public AudioClip clip;
    public string text;
    public float duration;
}
