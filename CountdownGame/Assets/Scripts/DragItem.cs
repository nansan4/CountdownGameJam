using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool dragging = false;
    [SerializeField] private bool canBeDragged = true;
    private Vector3 offset;

    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        if (!canBeDragged) return;

        if (ToolManager.Instance.currentTool != CurrentTool.Hand) return;

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        ToolManager.Instance.SetDraggedItem(this);
    }

    private void OnMouseUp()
    {
        dragging = false;
        ToolManager.Instance.SetDraggedItem(null);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Solder"))
        {
            canBeDragged = false;
            Debug.Log($"{gameObject.name} has been soldered in place.");
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
