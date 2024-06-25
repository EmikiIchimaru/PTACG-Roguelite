using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    public void GenerateWalls(int blocksPerRoom, float blockSize)
    {
        Debug.Log("generate walls");
        Vector3 topLeftCorner = new Vector3(
                transform.position.x - (0.5f * blocksPerRoom -1) * blockSize, 
                transform.position.y + (0.5f * blocksPerRoom -1) * blockSize, 0f);
        Vector3 botLeftCorner = new Vector3(
                transform.position.x - (0.5f * blocksPerRoom -1) * blockSize, 
                transform.position.y - (0.5f * blocksPerRoom -1) * blockSize, 0f);
        Vector3 botRightCorner = new Vector3(
                transform.position.x + (0.5f * blocksPerRoom -1) * blockSize, 
                transform.position.y - (0.5f * blocksPerRoom -1) * blockSize, 0f);
        
        for (int i = 0; i < blocksPerRoom-1; i++) 
        {
            GameObject wallGO1 = Instantiate(blockPrefab, topLeftCorner + new Vector3(i * blockSize, 0f, 0f), Quaternion.identity, transform);
            wallGO1.transform.localScale = new Vector2(blockSize, blockSize);
            GameObject wallGO2 = Instantiate(blockPrefab, botLeftCorner + new Vector3(i * blockSize, 0f, 0f), Quaternion.identity, transform);
            wallGO2.transform.localScale = new Vector2(blockSize, blockSize);
        }
        for (int j = 1; j < blocksPerRoom-2; j++) 
        {
            GameObject wallGO3 = Instantiate(blockPrefab, botLeftCorner + new Vector3(0f, j * blockSize, 0f), Quaternion.identity, transform);
            wallGO3.transform.localScale = new Vector2(blockSize, blockSize);
            GameObject wallGO4 = Instantiate(blockPrefab, botRightCorner + new Vector3(0f, j * blockSize, 0f), Quaternion.identity, transform);
            wallGO4.transform.localScale = new Vector2(blockSize, blockSize);
        }
    }
}
