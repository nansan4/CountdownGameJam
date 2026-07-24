using UnityEngine;

public class RearrangeToolBoxEvent : BossEventSO
{
    public override void Execute(BossManager bossManager)
    {
        // Logic to rearrange the toolbox
        Debug.Log("Rearranging Toolbox!");

        var buttons = ToolboxManager.Instance.toolboxButtons;
        for (int i = buttons.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (buttons[i], buttons[j]) = (buttons[j], buttons[i]);
        }
    }
}