using System;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBoardManager : MonoBehaviour
{
    [SerializeField] private List<ComponentDestination> destinationsList;

    public void CheckDestinations()
    {
        foreach(ComponentDestination destination in destinationsList)
        {
            if(destination.GetIsComponentPlaced())
            {
                continue;
            }
            else
            {
                return;
            }
        }
        // Debug.Log("Win!");
        GameState.Instance.ChangeGameStatus(GameStatus.Victory);
    }
}
