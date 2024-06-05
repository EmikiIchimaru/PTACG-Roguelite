using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    
    public int currentX;
    public int currentY;
    public WallGroup[] horiWalls;
    public WallGroup[] vertWalls;
    public Room[] rooms;
    public int offsetX;
    public int offsetY;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private float wallSize = 10f;
    [SerializeField] private float roomSize = 50f;
    [SerializeField] private int levelSize = 10;
    [SerializeField] private float wallChance = 0.2f;
    private int blocksPerRoom;


    // Start is called before the first frame update
    void Start()
    {
        horiWalls = new WallGroup[levelSize*(levelSize-1)];
        vertWalls = new WallGroup[levelSize*(levelSize-1)];
        rooms = new Room[levelSize*levelSize];
        //does not spawn in corner
        offsetX = Random.Range(1, levelSize-1)-levelSize;
        offsetY = Random.Range(1, levelSize-1)-levelSize;
        currentX = -(offsetX+1);
        currentY = -(offsetY+1);
        blocksPerRoom = (int) (roomSize / wallSize);
        //Debug.Log($"offsetX = {offsetX}, offsetY = {offsetY}");
        GenerateBoundary();
        GenerateWalls();
        GenerateRooms(); 
        //PlayerEnteredRoom(currentX,currentY);

        //ShowLevel(currentX,currentY);
    }

    private void GenerateBoundary()
    {
        for (int i = 0; i < levelSize; i++)
        {
            SetupWallGroup(i, 0, true, true);
            SetupWallGroup(i, levelSize, true, true);
        } 
        for (int j = 0; j < levelSize; j++)
        {
            SetupWallGroup(0, j, false, true);
            SetupWallGroup(levelSize, j, false, true);
        }
    }
    private void GenerateWalls()
    {
        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                //setup walls
                //Debug.Log($"{i}, {j}");
                //bool isEdgeX = (j == 0 || j == levelSize);
                //bool isEdgeY = (i == 0 || i == levelSize);
                if (j > 0) { horiWalls[XYToWallNumber(i,j,true)] = SetupWallGroup(i, j, true, false); }
                if (i > 0) { vertWalls[XYToWallNumber(i,j,false)] = SetupWallGroup(i, j, false, false); }
            }
        }
    }

    private void GenerateRooms()
    {
        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                //Debug.Log("setup room");
                SetupRoom(i,j); 
            }
        }
    }

    private WallGroup SetupWallGroup(int indexX, int indexY, bool isHorizontal, bool isBorder)
    {
        float pivotX = (0.5f + indexX + offsetX) * roomSize;
        float pivotY = (0.5f + indexY + offsetY) * roomSize;
        GameObject wallGroupGO = Instantiate(wallPrefab, new Vector2(pivotX,pivotY), Quaternion.identity);
        WallGroup wallGroup = wallGroupGO.GetComponent<WallGroup>();
        wallGroup.blocksPerRoom = blocksPerRoom;
        wallGroup.wallChance = wallChance;
        wallGroup.wallSize = wallSize;
        wallGroup.levelSize = levelSize;
        wallGroup.positionIndexX = indexX;
        wallGroup.positionIndexY = indexY;
        //wallGroup.pivotX = pivotX;
        //wallGroup.pivotY = pivotY;
        wallGroup.isHorizontal = isHorizontal;
        wallGroup.isBorder = isBorder;
        wallGroup.GenerateWall();
        if (!isBorder) { wallGroup.HideWall(); }
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

  

    public void PlayerEnteredRoom(int playerX, int playerY)
    {
        HideLevel(playerX,playerY);
        currentX = playerX;
        currentY = playerY;
        ShowLevel(playerX,playerY);

    }

    private void HideLevel(int playerX, int playerY)
    {
        foreach(int adjIndex in GetAdjacentRooms(currentX, currentY))
        {
            rooms[adjIndex].HideRoom();
        } 
        foreach(int wallIndex in GetHorizontalWalls(currentX,currentY))
        {
            horiWalls[wallIndex].HideWall();
            //Debug.Log($"delete horizontal wall {wallIndex}");
        }
        foreach(int wallIndex2 in GetVerticalWalls(currentX,currentY))
        {
            vertWalls[wallIndex2].HideWall();
            //Debug.Log($"delete vertical wall {wallIndex2}");
        }
    }

    private void ShowLevel(int playerX, int playerY)
    {
        foreach(int adjIndex1 in GetAdjacentRooms(currentX,currentY))
        {
            rooms[adjIndex1].ShowRoom();
            Debug.Log($"creating adj rooms at: {currentX},{currentY}");
        }
        foreach(int wallIndex3 in GetHorizontalWalls(currentX,currentY))
        {
            horiWalls[wallIndex3].ShowWall();
            //Debug.Log($"create horizontal wall {wallIndex3}");
        }
        foreach(int wallIndex4 in GetVerticalWalls(currentX,currentY))
        {
            vertWalls[wallIndex4].ShowWall();
            //Debug.Log($"create vertical wall {wallIndex4}");
        }
    }

    private List<int> GetHorizontalWalls(int indexX, int indexY)
    {
        List<int> horiWallsIndices = new List<int>();
    
        if (indexY > 1) { horiWallsIndices.Add(XYToWallNumber(indexX, indexY - 1, true)); }
        if (indexY > 0) 
        { 
            horiWallsIndices.Add(XYToWallNumber(indexX, indexY, true)); 
            if (indexX < levelSize - 1) { horiWallsIndices.Add(XYToWallNumber(indexX + 1, indexY, true)); }
            if (indexX > 0) { horiWallsIndices.Add(XYToWallNumber(indexX - 1, indexY, true)); }
        }
        if (indexY < levelSize - 1) 
        { 
            horiWallsIndices.Add(XYToWallNumber(indexX, indexY + 1, true));
            if (indexX < levelSize - 1) { horiWallsIndices.Add(XYToWallNumber(indexX + 1, indexY + 1, true)); }
            if (indexX > 0) { horiWallsIndices.Add(XYToWallNumber(indexX - 1, indexY + 1, true)); } 
        }
        if (indexY < levelSize - 2) { horiWallsIndices.Add(XYToWallNumber(indexX, indexY + 2, true)); }

        return horiWallsIndices;  
    }

    private List<int> GetVerticalWalls(int indexX, int indexY)
    {
        List<int> vertWallsIndices = new List<int>();
    
        if (indexX > 1) { vertWallsIndices.Add(XYToWallNumber(indexX - 1, indexY, false)); }
        if (indexX > 0) 
        { 
            vertWallsIndices.Add(XYToWallNumber(indexX, indexY, false)); 
            if (indexY < levelSize - 1) { vertWallsIndices.Add(XYToWallNumber(indexX, indexY + 1, false)); }
            if (indexY > 0) { vertWallsIndices.Add(XYToWallNumber(indexX, indexY - 1, false)); }
        }
        if (indexX < levelSize - 1) 
        { 
            vertWallsIndices.Add(XYToWallNumber(indexX + 1, indexY, false));
            if (indexY < levelSize - 1) { vertWallsIndices.Add(XYToWallNumber(indexX + 1, indexY + 1, false)); }
            if (indexY > 0) { vertWallsIndices.Add(XYToWallNumber(indexX + 1, indexY - 1, false)); } 
        }
        if (indexX < levelSize - 2) { vertWallsIndices.Add(XYToWallNumber(indexX + 2, indexY, false)); }

        return vertWallsIndices;  
    }


    private List<int> GetAdjacentRooms(int indexX, int indexY, bool include = true)
    {
        List<int> adjRoomsIndex = new List<int>();
        if (include) { adjRoomsIndex.Add(XYToRoomNumber(indexX, indexY)); }
        if (indexY < levelSize - 1) { adjRoomsIndex.Add(XYToRoomNumber(indexX, indexY + 1)); }
        if (indexY > 0) { adjRoomsIndex.Add(XYToRoomNumber(indexX, indexY - 1)); }
        //
        if (indexX > 0) 
        { 
            adjRoomsIndex.Add(XYToRoomNumber(indexX - 1, indexY));
            if (indexY < levelSize - 1) { adjRoomsIndex.Add(XYToRoomNumber(indexX - 1, indexY + 1)); }
            if (indexY > 0) { adjRoomsIndex.Add(XYToRoomNumber(indexX - 1, indexY - 1)); }
        }
        if (indexX < levelSize - 1) 
        { 
            adjRoomsIndex.Add(XYToRoomNumber(indexX + 1, indexY));
            if (indexY < levelSize - 1) { adjRoomsIndex.Add(XYToRoomNumber(indexX + 1, indexY + 1)); }
            if (indexY > 0) { adjRoomsIndex.Add(XYToRoomNumber(indexX + 1, indexY - 1)); }
        }

       return adjRoomsIndex;
    }

    private int XYToRoomNumber(int X, int Y)
    {
        return X + Y * (levelSize);
    }
    private int XYToWallNumber(int X, int Y, bool isHorizontal)
    {
        if(isHorizontal)
        {
            return X + (Y - 1) * (levelSize);
        }
        else
        {
            return X - 1 + Y * (levelSize - 1);
        }
        
    }
}
