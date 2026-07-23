using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SolderPoint : MonoBehaviour
{
    [SerializeField] private ComponentDestination destination;
    [SerializeField] private bool isSoldered;
    [SerializeField] private List<GameObject> solderedPoints;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Solder") && !solderedPoints.Contains(other.gameObject))
        {
            solderedPoints.Add(other.gameObject);
            CheckSoldering();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (solderedPoints.Contains(other.gameObject))
        {
            solderedPoints.Remove(other.gameObject);
            destination.IncrementSolderedPoints(-1);
        }
        if (solderedPoints.Count <= 0)
        {
            isSoldered = false;
        }
    }

    private void CheckSoldering()
    {
        if (!isSoldered)
        {
            isSoldered = true;
            destination.IncrementSolderedPoints(1);
        }
    }
}
