using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room Prefab Array", menuName = "Room Prefab Array")]
public class RoomPrefabSO : ScriptableObject
{
    public RoomContent tutorialRoom;
    public RoomContent bossRoom;
    public List<RoomContent> roomContents = new List<RoomContent>();

    public RoomContent GetRandomMaxRoomContent()
    {
        //List<RoomContent> roomContentsMax = new List<RoomContent>();
        int roomCount = roomContents.Count;
        int rng = Random.Range(1,4);
        
        return roomContents[roomCount-rng];
        
    }

}