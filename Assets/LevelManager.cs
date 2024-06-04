using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<WallGroup> wallGroups = new List<WallGroup>();
    public Room[] rooms;

    public int currentX;
    public int currentY;
    public int offsetX;
    public int offsetY;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private float wallSize = 10f;
    [SerializeField] private float roomSize = 50f;
    [SerializeField] private int levelSize = 10;
    private int blocksPerRoom;
    private float wallChance = 0.2f;



    // Start is called before the first frame update
    void Start()
    {
        blocksPerRoom = (int) (roomSize / wallSize);
        rooms = new Room[levelSize*levelSize];
        offsetX = Random.Range(0, levelSize)-levelSize;
        offsetY = Random.Range(0, levelSize)-levelSize;
        Debug.Log($"offsetX = {offsetX}, offsetY = {offsetY}");
        GenerateWalls();
        GenerateRooms(); 
    }

    private void GenerateWalls()
    {
        for (int i = 0; i <= levelSize; i++)
        {
            for (int j = 0; j <= levelSize; j++)
            {
                //setup walls
                Debug.Log($"{i}, {j}");
                bool isEdgeX = (j == 0 || j == levelSize);
                bool isEdgeY = (i == 0 || i == levelSize); 
                if (i < levelSize)
                {
                    if (i == levelSize-1)
                    {
                        SetupWallGroup(i, j, true, isEdgeX, 1);
                    }
                    else
                    {
                        SetupWallGroup(i, j, true, isEdgeX);
                    }
                }
                if (j < levelSize)
                {
                    SetupWallGroup(i, j, false, isEdgeY); 
                }

            }
        }
    }

    private void GenerateRooms()
    {
        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                Debug.Log("setup room");
                SetupRoom(i,j); 
            }
        }
    }



    private void SetupWallGroup(int indexX, int indexY, bool isHorizontal, bool isEdge, int extend = 0)
    {
        
        float pivotX = (0.5f + indexX + offsetX) * roomSize;
        float pivotY = (0.5f + indexY + offsetY) * roomSize;
        GameObject wallGroupGO = Instantiate(wallPrefab, new Vector2(pivotX,pivotY), Quaternion.identity);
        WallGroup wallGroup = wallGroupGO.GetComponent<WallGroup>();
        wallGroups.Add(wallGroup);
        wallGroup.blocksPerRoom = blocksPerRoom;
        wallGroup.wallChance = wallChance;
        wallGroup.wallSize = wallSize;
        wallGroup.positionIndexX = indexX;
        wallGroup.positionIndexY = indexY;
        wallGroup.pivotX = pivotX;
        wallGroup.pivotY = pivotY;
        wallGroup.isHorizontal = isHorizontal;
        wallGroup.isEdge = isEdge;
        wallGroup.extend = extend;
        //wallGroup.GenerateWall();
    }

    private void SetupRoom(int indexX, int indexY)
    {
        float pivotX = (1f + indexX + offsetX) * roomSize;
        float pivotY = (1f + indexY + offsetY) * roomSize;
        GameObject roomGO = Instantiate(roomPrefab, new Vector2(pivotX,pivotY), Quaternion.identity);
        Room room = roomGO.GetComponent<Room>();
        int tempRoomNumber = XYToRoomNumber(indexX,indexY);
        room.roomNumber = tempRoomNumber;
        room.positionIndexX = indexX;
        room.positionIndexY = indexY;
        rooms[tempRoomNumber] = room;
    }

    public void PlayerEnteredRoom(int roomNumber, int playerX, int playerY)
    {
        foreach(WallGroup wallGroup in wallGroups)
        {
            wallGroup.UpdateWall(playerX, playerY);
        }
        foreach(Room room in GetAdjacentRooms(currentX, currentY))
        {
            room.HideRoom();
        }
        currentX = playerX;
        currentY = playerY;
        foreach(Room room in GetAdjacentRooms(currentX,currentY))
        {
            room.ShowRoom();
        }

    }

    private List<Room> GetAdjacentRooms(int indexX, int indexY, bool include = true)
    {
       List<Room> adjRooms = new List<Room>();
       if (include) { adjRooms.Add(rooms[XYToRoomNumber(indexX,indexY)]); }
       //get right
       if (indexX < levelSize) { adjRooms.Add(rooms[XYToRoomNumber(indexX+1,indexY)]); }
       return adjRooms;
    }

    private int XYToRoomNumber(int X, int Y)
    {
        return X + Y * levelSize;
    }
}
