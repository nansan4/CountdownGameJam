using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
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
    private InputSystem_Actions inputActions;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> solderSpawnSounds;
    private ToolboxManager toolboxManager;
    

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

    #region Input Management
    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        SubscribeInputs();

        toolboxManager = ToolboxManager.Instance;
    }

    void OnDisable()
    {
        inputActions.Disable();
        UnsubscribeInputs();
    }

    void SubscribeInputs()
    {
        inputActions.Player.LeftClick.performed += HandleLeftClick;
        inputActions.Player.RightClick.performed += HandleRightClick;
        inputActions.Player.ToolHotkey.performed += HandleToolHotkey;
    }

    void UnsubscribeInputs()
    {
        inputActions.Player.LeftClick.performed -= HandleLeftClick;
        inputActions.Player.RightClick.performed -= HandleRightClick;
        inputActions.Player.ToolHotkey.performed -= HandleToolHotkey;
    }

    #endregion

    public void SetCurrentTool(CurrentTool tool)
    {
        currentTool = tool;
        Debug.Log($"Current tool set to: {currentTool}");
    }

    #region Click Handling

    private void HandleLeftClick(InputAction.CallbackContext context)
    {
        switch (currentTool)
        {
            case CurrentTool.Hand:
                //Do nothing for now
                break;
            case CurrentTool.SolderingIron:
                // Logic for Soldering Iron tool click
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -Camera.main.transform.position.z));
                worldPos.z = 0f;
                SpawnSolder(worldPos);
                break;
            case CurrentTool.Pliers:
                // Logic for Pliers tool click
                Debug.Log("Pliers tool clicked.");
                break;
            case CurrentTool.MagnifyingGlass:
                // Logic for Magnifying Glass tool click
                Debug.Log("Magnifying Glass tool clicked.");
                break;
            case CurrentTool.DirectionalInstrument:
                // Logic for Directional Instrument tool click
                Debug.Log("Directional Instrument tool clicked.");
                break;
            default:
                Debug.LogWarning("Unhandled tool click.");
                break;
        }
    }

    private void HandleToolHotkey(InputAction.CallbackContext context)
    {
        if (int.TryParse(context.control.name, out int key))
        {
            toolboxManager.HandleToolboxHotkey(key - 1);
        }
    }

    private void HandleRightClick(InputAction.CallbackContext context)
    {
        switch (currentTool)
        {
            case CurrentTool.Hand:
                // Logic for Hand tool right-click
                Debug.Log("Hand tool right-clicked.");
                break;
            case CurrentTool.SolderingIron:
                // Logic for Soldering Iron tool right-click
                TryDestroySolder();
                break;
            case CurrentTool.Pliers:
                // Logic for Pliers tool right-click
                Debug.Log("Pliers tool right-clicked.");
                break;
            case CurrentTool.MagnifyingGlass:
                // Logic for Magnifying Glass tool right-click
                Debug.Log("Magnifying Glass tool right-clicked.");
                break;
            case CurrentTool.DirectionalInstrument:
                // Logic for Directional Instrument tool right-click
                Debug.Log("Directional Instrument tool right-clicked.");
                break;
            default:
                Debug.LogWarning("Unhandled tool right-click.");
                break;
        }
    }

    #endregion

    #region Hand Tool

    public void SetDraggedItem(DragItem item)
    {
        if (currentTool != CurrentTool.Hand)
        {
            Debug.LogWarning("Cannot drag items unless the Hand tool is selected.");
            return;
        }

        draggedItem = item;

        if (item != null) Debug.Log($"Dragging item: {item.name}");
        else Debug.Log("Stopped dragging.");
    }

    #endregion
    #region Soldering Iron Tool

    private void SpawnSolder(Vector3 position)
    {
        // Logic to spawn solder at the given position
        Instantiate(solderPrefab, position, Quaternion.identity);
        Debug.Log($"Spawning solder at: {position}");
        audioSource.PlayOneShot(solderSpawnSounds[UnityEngine.Random.Range(0, solderSpawnSounds.Count)]);
    }

    private void TryDestroySolder()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        if (hit != null && hit.CompareTag("Solder"))
        {
            DestroySolder(hit.gameObject);
        }
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
