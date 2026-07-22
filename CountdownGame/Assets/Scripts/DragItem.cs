using UnityEngine;

public class DragItem : MonoBehaviour
{
    private bool dragging = false;
    private bool canBeDragged = true;
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
        //Record difference between objects center and point on object clicked by mouse
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}
