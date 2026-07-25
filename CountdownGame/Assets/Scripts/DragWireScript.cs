using UnityEngine;
using System.Collections.Generic;
public class DragWireScript : MonoBehaviour
{
    public Transform P2;
    public float vertexCount = 3;
    public float bendStrength = 1;
    public LineRenderer lineRenderer;

    private CircleCollider2D endColl;

    private void Start()
    {
        //OnMouseDrag is called via event when a GUIElement or Collider is clicked, so we need a collider
        //  that the player will click on to have that event be called
        endColl = gameObject.AddComponent<CircleCollider2D>();
        endColl.radius = 0.5f;
        endColl.offset = P2.transform.position;
    }

    private void OnMouseDrag()
    {
        // if (ToolManager.Instance.currentTool != CurrentTool.Pliers) return;
        List<Vector3> points = new List<Vector3>();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //place p2 at the midpoint of the line to use in bending code below
        P2.transform.position = new Vector3((transform.position.x + mousePos.x) / 2, (transform.position.y + mousePos.y) / 2 + bendStrength, 0);

        //lerp positions of points to bend line
        for (float i = 0; i <= 1; i += 1 / vertexCount)
        {
            Vector3 t1 = Vector3.Lerp(transform.position, P2.position, i);
            Vector3 t2 = Vector3.Lerp(P2.position, mousePos, i);
            Vector3 point = Vector3.Lerp(t1, t2, i);
            points.Add(point);
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

        
        //set the collider to be at the end of the line
        //I'm not sure if I should use mousePos, since there could be an edge case where the collider gets set away from the end of the cable
        endColl.offset = new Vector3((transform.position.x + mousePos.x), (transform.position.y + mousePos.y), 0);

    }
}
