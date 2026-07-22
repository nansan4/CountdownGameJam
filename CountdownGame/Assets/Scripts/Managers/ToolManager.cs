using UnityEngine;

public enum CurrentTool
{
    Hand,
    SolderingIron,
    Pliers,
    MagnifyingGlass,
    DirectionalInstrument
}

public class ToolManager : MonoBehaviour
{
    public CurrentTool currentTool = CurrentTool.Hand;
    [SerializeField] private DragItem draggedItem;
    [SerializeField] private GameObject solderPrefab;

    #region Singleton
    public static ToolManager Instance { get; private set; }
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

    public void SetCurrentTool(CurrentTool tool)
    {
        currentTool = tool;
        Debug.Log($"Current tool set to: {currentTool}");
    }

    #region Hand Tool

    public void SetDraggedItem(DragItem item)
    {
        if (currentTool == CurrentTool.Hand)
        {
            // Logic to set the dragged item
            draggedItem = item;
            Debug.Log($"Dragging item: {item.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("Cannot drag items unless the Hand tool is selected.");
        }
    }

    #endregion
    #region Soldering Iron Tool

    private void SpawnSolder(Vector3 position)
    {
        // Logic to spawn solder at the given position
        Instantiate(solderPrefab, position, Quaternion.identity);
        Debug.Log($"Spawning solder at: {position}");
    }

    private void DestroySolder(GameObject solder)
    {
        // Logic to destroy the solder object
        Destroy(solder);
    }

    #endregion
    #region Pliers Tool

    #endregion
    #region Magnifying Glass Tool

    #endregion
    #region Directional Instrument Tool

    #endregion
}
