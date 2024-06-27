using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntity : MonoBehaviour
{
    public int positionIndexX;
    public int positionIndexY;
    
    public bool isPlayerInSameRoom { get { return GetPlayerInSameRoom(); } }

    private bool GetPlayerInSameRoom()
    {
        return (positionIndexX == LevelManager.Instance.currentX && positionIndexY == LevelManager.Instance.currentY);
    }
    
}
