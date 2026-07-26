using UnityEngine;
public abstract class BossEventSO : ScriptableObject
{
    [SerializeField] private string eventName;
    public abstract void Execute(BossManager bossManager);
}
