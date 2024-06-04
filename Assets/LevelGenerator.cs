using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int offsetX;
    public int offsetY;

    public List<WallGroup> wallGroups = new List<WallGroup>();

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
        offsetX = Random.Range(0, levelSize)-levelSize;
        offsetY = Random.Range(0, levelSize)-levelSize;
        Debug.Log($"offsetX = {offsetX}, offsetY = {offsetY}");
        GenerateLevel();
        
    }

    private void GenerateLevel()
    {
        for (int i = 0; i <= levelSize; i++)
        {
            for (int j = 0; j <= levelSize; j++)
            {
                //setup walls
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
                
                if (j < levelSize) { SetupWallGroup(i, j, false, isEdgeY); }
                //setup rooms


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
        wallGroup.levelGen = this;
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
    }

    private void SetupRoom(int indexX, int indexY)
    {
        float pivotX = (0.5f + indexX + offsetX) * roomSize;
        float pivotY = (0.5f + indexY + offsetY) * roomSize;
        GameObject roomGO = Instantiate(roomPrefab, new Vector2(pivotX,pivotY), Quaternion.identity);
        Room room = roomGO.GetComponent<Room>();
        room.levelGen = this;
    }

}
