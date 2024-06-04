using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGroup : MonoBehaviour
{
    
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject borderPrefab;

    public bool isBorder;
    public bool isHorizontal;

    public int positionIndexX;
    public int positionIndexY;

    public int blocksPerRoom;
    public float wallChance;
    public float wallSize;

    public float levelSize;
    

    private int door = -1;

/*     void Start()
    {
        blocksPerRoom = LevelManager.Instance.blocksPerRoom;
        wallChance = LevelManager.Instance.wallChance;
        wallSize = LevelManager.Instance.wallSize;
    } */

    public void GenerateWall()
    {
        
        if (isBorder) 
        {
            GenerateOuterWall();
        }
        else
        {
            GenerateInnerWall();
        }
    }

    private void GenerateInnerWall()
    {
        if (door == -1) { door = Random.Range(1, blocksPerRoom-2); }
        Vector3 wallVector = (isHorizontal)?new Vector2(wallSize,0f): new Vector2(0f, wallSize);
        bool isWalled = (Random.Range(0f,1f) > wallChance);
        for (int i = 0; i < blocksPerRoom; i++) 
        {
            if (!isHorizontal && i == 0) { continue; }
            if (positionIndexX == 0 && i == 0) { continue; }
            if ((i == door || i == door+1) && isWalled) { continue; }
            GameObject wallGO = Instantiate(blockPrefab, new Vector3(transform.position.x,transform.position.y,0f) + i * wallVector, Quaternion.identity, transform);
            wallGO.transform.localScale = new Vector2(wallSize, wallSize);
            //Debug.Log("gen inner wall");
            //wallGO.GetComponent<WallBlock>().SetupBlock(isEdge);
        }
    }

    private void GenerateOuterWall()
    {
        Vector3 wallVector = (isHorizontal)?new Vector2(wallSize,0f): new Vector2(0f, wallSize);
        int extend = GetExtend();
        for (int i = 0; i < blocksPerRoom + extend; i++) 
        {
            if (!isHorizontal && i == 0) { continue; }
            GameObject wallGO = Instantiate(borderPrefab, new Vector3(transform.position.x,transform.position.y,0f) + i * wallVector, Quaternion.identity, transform);
            wallGO.transform.localScale = new Vector2(wallSize, wallSize);
            //Debug.Log("gen outer wall");
            //wallGO.GetComponent<WallBlock>().SetupBlock(isEdge);
        }
    }

    public void ShowWall()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void HideWall()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private int GetExtend()
    {
        if (positionIndexX == 0 && positionIndexY == levelSize-1) { return 0; }
        if (positionIndexX == levelSize-1 && positionIndexY == 0) { return 1; }
        if (isHorizontal)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
/* 
    private bool ShouldGenerateWall(int playerX, int playerY)
    {
        //check if too far
        if ((Mathf.Abs(positionIndexX-playerX) > 2) || ((Mathf.Abs(positionIndexY-playerY) > 2))) { return false; }
        //check for both side
        bool adjRow = ((Mathf.Abs(positionIndexX-playerX) < 2) && (positionIndexY == playerY));
        bool adjCol = ((Mathf.Abs(positionIndexY-playerY) < 2) && (positionIndexX == playerX));
        bool topRight = ((positionIndexX == playerX + 1) && (positionIndexY == playerY + 1));
        //check for one side
        return (adjRow || adjCol || topRight);
    } */


}
