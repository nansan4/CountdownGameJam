using UnityEngine;
[CreateAssetMenu(fileName = "New Boss Event", menuName = "Boss Event", order = 2)]
public abstract class BossEventSO : ScriptableObject
{
    public abstract void Execute(BossManager bossManager);
}
