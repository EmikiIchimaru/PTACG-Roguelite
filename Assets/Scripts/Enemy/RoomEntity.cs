using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntity : MonoBehaviour
{
    public Room room { get; set; }
    public int positionIndexX;
    public int positionIndexY;
    
    public bool isPlayerInSameRoom { get { return GetPlayerInSameRoom(); } }

    private bool GetPlayerInSameRoom()
    {
        return (positionIndexX == LevelManager.Instance.currentX && positionIndexY == LevelManager.Instance.currentY);
    }

    private void OnDestroy()
    {
        // change room colour
    }
    
}
