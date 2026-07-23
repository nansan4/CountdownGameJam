using UnityEngine;
using System.Collections.Generic;
public class DragWireScript : MonoBehaviour
{
    public Transform P2;
    public float vertexCount = 3;
    public float bendStrength = 1;
    public LineRenderer lineRenderer;

    private void OnMouseDrag()
    {
        // if (ToolManager.Instance.currentTool != CurrentTool.Pliers) return;
        List<Vector3> points = new List<Vector3>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        P2.transform.position = new Vector3((transform.position.x + mousePos.x) / 2, (transform.position.y + mousePos.y) / 2 + bendStrength, 0);

        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            Vector3 t1 = Vector3.Lerp(transform.position, P2.position, i);
            Vector3 t2 = Vector3.Lerp(P2.position, mousePos, i);
            Vector3 point = Vector3.Lerp(t1, t2, i);
            points.Add(point);
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
