using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool dragging = false;
    [SerializeField] private bool canBeDragged = true;
    private Vector3 offset;
    [SerializeField] private ComponentDestination componentDestination;
    [SerializeField] private ComponentScript componentScript;

    public ComponentDestination Destination { get { return componentDestination; } }

    void Start()
    {
        componentScript = GetComponent<ComponentScript>();
    }

    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            ToolManager.Instance.CheckDistance(this);

        }
    }

    private void OnMouseDown()
    {
        if (!canBeDragged) return;

        if (ToolManager.Instance.currentTool != CurrentTool.Hand) return;

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        ToolManager.Instance.SetDraggedItem(this);
        ToolManager.Instance.CheckDistance(this);
    }

    private void OnMouseUp()
    {
        dragging = false;
        ToolManager.Instance.SetDraggedItem(null);

        ToolManager.Instance.CheckDistance(this); //guarantee null case is hit to display status of 'no item to check'
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destination"))
        {
            componentDestination = other.GetComponent<ComponentDestination>();
            if (componentDestination.componentName != componentScript.componentName)
            {
                componentDestination = null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Solder"))
        {
            canBeDragged = false;
            // Debug.Log($"{gameObject.name} has been soldered in place.");
            // if (componentDestination != null)
            // {
            //     componentDestination.SetComponentPlaced(true);
            // }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Solder"))
        {
            canBeDragged = true;
            Debug.Log($"{gameObject.name} can be dragged again.");
        }
    }
}
