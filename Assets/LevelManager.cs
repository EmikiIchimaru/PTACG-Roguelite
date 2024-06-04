using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public WallGroup[] horiWalls;
    public WallGroup[] vertWalls;
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
        horiWalls = new WallGroup[levelSize*(levelSize+1)];
        vertWalls = new WallGroup[levelSize*(levelSize+1)];
        rooms = new Room[levelSize*levelSize];
        offsetX = Random.Range(0, levelSize)-levelSize;
        offsetY = Random.Range(0, levelSize)-levelSize;
        blocksPerRoom = (int) (roomSize / wallSize);
        //Debug.Log($"offsetX = {offsetX}, offsetY = {offsetY}");
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
                        horiWalls[XYToRoomNumber(i,j)] = SetupWallGroup(i, j, true, isEdgeX, 1);
                    }
                    else
                    {
                        horiWalls[XYToRoomNumber(i,j)] = SetupWallGroup(i, j, true, isEdgeX);
                    }
                }
                if (j < levelSize)
                {
                    vertWalls[XYToWallNumber(i,j)] = SetupWallGroup(i, j, false, isEdgeY); 
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



    private WallGroup SetupWallGroup(int indexX, int indexY, bool isHorizontal, bool isEdge, int extend = 0)
    {
        float pivotX = (0.5f + indexX + offsetX) * roomSize;
        float pivotY = (0.5f + indexY + offsetY) * roomSize;
        GameObject wallGroupGO = Instantiate(wallPrefab, new Vector2(pivotX,pivotY), Quaternion.identity);
        WallGroup wallGroup = wallGroupGO.GetComponent<WallGroup>();
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
        wallGroup.GenerateWall();
        return wallGroup;
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
        foreach(int adjIndex in GetAdjacentRooms(currentX, currentY))
        {
            rooms[adjIndex].HideRoom();
        } 
/*         foreach(WallGroup wallGroup in wallGroups)
        {
            wallGroup.UpdateWall(playerX, playerY);
        } */
        currentX = playerX;
        currentY = playerY;
        foreach(int adjIndex in GetAdjacentRooms(currentX,currentY))
        {
            rooms[adjIndex].ShowRoom();
        }

    }

    private List<int> GetHorizontalWalls(int indexX, int indexY)
    {
        List<int> horiWallsIndices = new List<int>();
      
        if (indexX < levelSize) { horiWallsIndices.Add(XYToRoomNumber(indexX,indexY)); }
        return horiWallsIndices;  
    }

    private List<int> GetVerticalWalls(int indexX, int indexY)
    {
        List<int> vertWallsIndices = new List<int>();
      
        if (indexX < levelSize) { vertWallsIndices.Add(XYToWallNumber(indexX,indexY)); }
        return vertWallsIndices;  
    }


    private List<int> GetAdjacentRooms(int indexX, int indexY, bool include = true)
    {
       List<int> adjRoomsIndex = new List<int>();
       if (include) { adjRoomsIndex.Add(XYToRoomNumber(indexX,indexY)); }
       //get right
       //if (indexX < levelSize) { adjRooms.Add(rooms[XYToRoomNumber(indexX+1,indexY)]); }
       return adjRoomsIndex;
    }

    private int XYToRoomNumber(int X, int Y)
    {
        return X + Y * levelSize;
    }
    private int XYToWallNumber(int X, int Y)
    {
        return X + Y * (levelSize+1);
    }
}
