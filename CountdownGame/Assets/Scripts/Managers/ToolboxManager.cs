using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class ToolboxManager : MonoBehaviour
{
    [SerializeField] private List<Button> toolboxButtons;
    [SerializeField] private Button selectedButton;

    #region Singleton
    public static ToolboxManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        OnToolboxButtonClick(toolboxButtons[0]); //Set default selected button
    }
    #endregion

    #region Hotkey Handling

    public void HandleToolboxHotkey(int toolIndex)
    {
        if (toolIndex < 0 || toolIndex >= toolboxButtons.Count)
        {
            Debug.LogWarning($"Tool index {toolIndex} is out of range.");
            return;
        }

        OnToolboxButtonClick(toolboxButtons[toolIndex]);
    }

    #endregion

    #region Button Callbacks
    public void OnToolboxButtonClick(Button clickedButton)
    {
        if (clickedButton == selectedButton) return;

        selectedButton = clickedButton;
        selectedButton.GetComponent<Outline>().enabled = true;
        Debug.Log($"Selected tool: {selectedButton.name}");

        foreach (Button button in toolboxButtons) //Make sure no other buttons are outlined
        {
            if (button == selectedButton) continue;
            button.GetComponent<Outline>().enabled = false;
        }

        //Set the current tool in ToolManager based on the clicked button
        switch (selectedButton.name)
        {
            case "HandButton":
                ToolManager.Instance.SetCurrentTool(CurrentTool.Hand);
                break;
            case "SolderingButton":
                ToolManager.Instance.SetCurrentTool(CurrentTool.SolderingIron);
                break;
            case "PliersButton":
                ToolManager.Instance.SetCurrentTool(CurrentTool.Pliers);
                break;
            case "MagnifyingButton":
                ToolManager.Instance.SetCurrentTool(CurrentTool.MagnifyingGlass);
                break;
            case "DirectionalButton":
                ToolManager.Instance.SetCurrentTool(CurrentTool.DirectionalInstrument);
                break;
            default:
                Debug.LogWarning("Unknown button clicked.");
                break;
        }
    }
    #endregion
}
