using UnityEngine;

public class DragGlass : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        if (ToolManager.Instance.currentTool != CurrentTool.Hand) return;

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}
