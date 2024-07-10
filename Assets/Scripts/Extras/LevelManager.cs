using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Generation Parameters")]
    [SerializeField] private float blockSize;
    [SerializeField] private float roomSize;
    [SerializeField] private int levelSize;
    [SerializeField] private float wallChance;

    [Header("Prefabs")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject roomPrefab;


    //[Header("Player Position")]
    public int currentX { get; private set; }
    public int currentY { get; private set; }
    public int startX;
    public int startY;

    [Header("Misc")]
    public WallGroup[] horiWalls;
    public WallGroup[] vertWalls;
    public Room[] rooms;

    public GameObject wallParent;
    public GameObject roomParent;
    private int offsetX;
    private int offsetY;
    private int blocksPerRoom;
    
    //private bool isBossFight;

    // Start is called before the first frame update
    void Start()
    {
        //isBossFight = false;
        
        horiWalls = new WallGroup[levelSize*(levelSize-1)];
        vertWalls = new WallGroup[levelSize*(levelSize-1)];
        rooms = new Room[levelSize*levelSize];
        //does not spawn in corner
        offsetX = Random.Range(2, levelSize-1)-levelSize;
        offsetY = Random.Range(2, levelSize-1)-levelSize;
        currentX = -(offsetX+1);
        currentY = -(offsetY+1);
        startX = currentX;
        startY = currentY;
        blocksPerRoom = (int) (roomSize / blockSize);
        //Debug.Log($"offsetX = {offsetX}, offsetY = {offsetY}");
        GenerateBoundary();
        GenerateWalls();
        GenerateRooms(); 
        //PlayerEnteredRoom(currentX,currentY);
        
        ShowLevel(currentX,currentY);
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
        GameObject wallGroupGO = Instantiate(wallPrefab, new Vector2(pivotX,pivotY), Quaternion.identity, wallParent.transform);
        WallGroup wallGroup = wallGroupGO.GetComponent<WallGroup>();
        wallGroup.blocksPerRoom = blocksPerRoom;
        wallGroup.wallChance = wallChance;
        wallGroup.blockSize = blockSize;
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
        GameObject roomGO = Instantiate(roomPrefab, new Vector2(pivotX,pivotY), Quaternion.identity, roomParent.transform);
        Room room = roomGO.GetComponent<Room>();
        int tempRoomNumber = XYToRoomNumber(indexX,indexY);
        room.roomNumber = tempRoomNumber;
        room.positionIndexX = indexX;
        room.positionIndexY = indexY;
        rooms[tempRoomNumber] = room;
        room.SetDifficultyLevel(indexX,indexY);
    }

  

    public void PlayerEnteredRoom(int playerX, int playerY)
    {
        //if (isBossFight) { return; }
        if (currentX == playerX && currentY == playerY) { return; }
        HideLevel(playerX,playerY);
        currentX = playerX;
        currentY = playerY;
        ShowLevel(playerX,playerY);
        GameManager.Instance.BossCountDown();
    }

    public void InitializeBossRoom()
    {
        //isBossFight = true;
        Room room = rooms[XYToRoomNumber(currentX, currentY)];
        room.ReplaceWithBossRoom();
        //GameObject bossRoomGO = Instantiate(bossRoomPrefab, room.transform.position, Quaternion.identity);
        //BossRoom bossRoom = bossRoomGO.GetComponent<BossRoom>();
        //bossRoom.GenerateWalls(blocksPerRoom, blockSize);
    }
    public void GenerateBossWalls(Vector3 bossRoomPosition, GameObject blockPrefab)
    {
        Debug.Log("generate walls");
        Vector3 topLeftCorner = new Vector3(
                bossRoomPosition.x - (0.5f * blocksPerRoom - 0.5f) * blockSize, 
                bossRoomPosition.y + (0.5f * blocksPerRoom - 0.5f) * blockSize, 0f);
        Vector3 botLeftCorner = new Vector3(
                bossRoomPosition.x - (0.5f * blocksPerRoom - 0.5f) * blockSize, 
                bossRoomPosition.y - (0.5f * blocksPerRoom - 0.5f) * blockSize, 0f);
        Vector3 botRightCorner = new Vector3(
                bossRoomPosition.x + (0.5f * blocksPerRoom - 0.5f) * blockSize, 
                bossRoomPosition.y - (0.5f * blocksPerRoom - 0.5f) * blockSize, 0f);
        
        for (int i = 0; i < blocksPerRoom; i++) 
        {
            GameObject wallGO1 = Instantiate(blockPrefab, topLeftCorner + new Vector3(i * blockSize, 0f, 0f), Quaternion.identity, transform);
            wallGO1.transform.localScale = new Vector2(blockSize, blockSize);
            GameObject wallGO2 = Instantiate(blockPrefab, botLeftCorner + new Vector3(i * blockSize, 0f, 0f), Quaternion.identity, transform);
            wallGO2.transform.localScale = new Vector2(blockSize, blockSize);
        }
        for (int j = 1; j < blocksPerRoom-1; j++) 
        {
            GameObject wallGO3 = Instantiate(blockPrefab, botLeftCorner + new Vector3(0f, j * blockSize, 0f), Quaternion.identity, transform);
            wallGO3.transform.localScale = new Vector2(blockSize, blockSize);
            GameObject wallGO4 = Instantiate(blockPrefab, botRightCorner + new Vector3(0f, j * blockSize, 0f), Quaternion.identity, transform);
            wallGO4.transform.localScale = new Vector2(blockSize, blockSize);
        }
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
            //Debug.Log($"creating adj rooms at: {currentX},{currentY}");
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
    
        if (indexY > 1)
        { 
            horiWallsIndices.Add(XYToWallNumber(indexX, indexY - 1, true)); 
            if (indexX < levelSize - 1) { horiWallsIndices.Add(XYToWallNumber(indexX + 1, indexY - 1, true)); }
            if (indexX > 0) { horiWallsIndices.Add(XYToWallNumber(indexX - 1, indexY - 1, true)); }
        }
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
        if (indexY < levelSize - 2) 
        { 
            horiWallsIndices.Add(XYToWallNumber(indexX, indexY + 2, true));
            if (indexX < levelSize - 1) { horiWallsIndices.Add(XYToWallNumber(indexX + 1, indexY + 2, true)); }
            if (indexX > 0) { horiWallsIndices.Add(XYToWallNumber(indexX - 1, indexY + 2, true)); }
        }

        return horiWallsIndices;  
    }

    private List<int> GetVerticalWalls(int indexX, int indexY)
    {
        List<int> vertWallsIndices = new List<int>();
    
        if (indexX > 1) 
        { 
            vertWallsIndices.Add(XYToWallNumber(indexX - 1, indexY, false)); 
            if (indexY < levelSize - 1) { vertWallsIndices.Add(XYToWallNumber(indexX - 1, indexY + 1, false)); }
            if (indexY > 0) { vertWallsIndices.Add(XYToWallNumber(indexX - 1, indexY - 1, false)); }
        }
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
        if (indexX < levelSize - 2) 
        { 
            vertWallsIndices.Add(XYToWallNumber(indexX + 2, indexY, false)); 
            if (indexY < levelSize - 1) { vertWallsIndices.Add(XYToWallNumber(indexX + 2, indexY + 1, false)); }
            if (indexY > 0) { vertWallsIndices.Add(XYToWallNumber(indexX + 2, indexY - 1, false)); }
        }

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
