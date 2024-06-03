using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private float wallSize = 10f;
    private float roomSize = 100f;
    private int blocksPerRoom;
    private float wallChance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        blocksPerRoom = (int) (roomSize / wallSize);
        int offsetX = Random.Range(0, 10)-10;
        int offsetY = Random.Range(0, 10)-10;
        Debug.Log($"offsetX = {offsetX}, offsetY = {offsetY}");
        for (int i = 0; i <= 10; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                bool isEdgeX = (j == 0 || j == 10);
                bool isEdgeY = (i == 0 || i == 10);
                if (i < 10) 
                { 
                    if (i == 9)
                    {
                        GenerateWall((0.5f + i + offsetX) * roomSize, (0.5f + j + offsetY) * roomSize, true, isEdgeX, 1);
                    }
                    else
                    {
                        GenerateWall((0.5f + i + offsetX) * roomSize, (0.5f + j + offsetY) * roomSize, true, isEdgeX);
                    }
                     
                }
                
                if (j < 10) { GenerateWall((0.5f + i + offsetX) * roomSize, (0.5f + j + offsetY) * roomSize, false, isEdgeY); }
            }
        }
        
    }

    private void GenerateWall(float pivotX, float pivotY, bool isHorizontal, bool isEdge, int extend = 0)
    {
        int door = Random.Range(1, blocksPerRoom-2);
        Vector3 wallVector = (isHorizontal)?new Vector2(wallSize,0f): new Vector2(0f, wallSize);
        bool isWalled = (Random.Range(0f,1f) > wallChance);
        for (int i = 0; i < blocksPerRoom + extend; i++) 
        {
            if (!isHorizontal && i == 0) { continue; }
            if (!isEdge && (i == door || i == door+1) && isWalled) { continue; }
            GameObject wallGO = Instantiate(blockPrefab, new Vector3(pivotX,pivotY,0f) + i * wallVector, Quaternion.identity, transform);
            
            wallGO.GetComponent<WallBlock>().SetupBlock(isEdge);
        }
    }

}
