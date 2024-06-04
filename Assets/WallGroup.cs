using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGroup : MonoBehaviour
{
    
    [SerializeField] private GameObject blockPrefab;

    public int positionIndexX;
    public int positionIndexY;

    public int blocksPerRoom;
    public float wallChance;
    public float wallSize;

    public float pivotX;
    public float pivotY;
    public bool isHorizontal;

    private int door = -1;

    public void GenerateWall()
    {
        if (door == -1) { door = Random.Range(1, blocksPerRoom-2); }
        Vector3 wallVector = (isHorizontal)?new Vector2(wallSize,0f): new Vector2(0f, wallSize);
        bool isWalled = (Random.Range(0f,1f) > wallChance);
        for (int i = 0; i < blocksPerRoom; i++) 
        {
            if (!isHorizontal && i == 0) { continue; }
            if ((i == door || i == door+1) && isWalled) { continue; }
            GameObject wallGO = Instantiate(blockPrefab, new Vector3(pivotX,pivotY,0f) + i * wallVector, Quaternion.identity, transform);
            wallGO.transform.localScale = new Vector2(wallSize, wallSize);
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
