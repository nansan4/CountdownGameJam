using Unity.VisualScripting;
using UnityEngine;

public class ComponentDestination : MonoBehaviour
{
    [SerializeField] private CircuitBoardManager board;
    [SerializeField] private ComponentScript component;
    [SerializeField] private bool componentPlaced = false;
    public string componentName;

    [Header("Solder Variables")]
    [SerializeField] private int totalSolderPoints;
    [SerializeField] private int currentSolderedPointsCount;

    public bool GetIsComponentPlaced()
    {
        return componentPlaced;
    }

    public void SetComponentPlaced(bool placed)
    {
        componentPlaced = placed;
        // if (placed)
        // {
        //     board.CheckDestinations();
        // }
    }

    private void CheckComponentSecured()
    {
        if (currentSolderedPointsCount == totalSolderPoints)
        {
            board.CheckDestinations();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Solder"))
        {
            return;
        }
        else if (other.CompareTag("Component"))
        {
            component = other.TryGetComponent<ComponentScript>(out ComponentScript componentScript) ? componentScript : null;
        }
    }

    public void IncrementSolderedPoints(int num)
    {
        currentSolderedPointsCount += num;
        if (currentSolderedPointsCount > 0)
        {
            SetComponentPlaced(true);
        }
        CheckComponentSecured();
    }
}
