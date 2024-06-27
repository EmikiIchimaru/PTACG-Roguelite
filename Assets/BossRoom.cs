using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject bossPrefab;

    public void StartBossFight()
    {
        Instantiate(bossPrefab, transform.position + new Vector3(0f, 20f, 0f), Quaternion.identity, transform);
        UIManager.Instance.ShowBossHUD();
        //Health bossHealth = bossPrefab.GetComponent<Health>();
        //bossHealth.CurrentHealth = bossHealth.MaxHealth;
        StartCoroutine(ZoomOutCoroutine());

    }

    IEnumerator ZoomOutCoroutine()
    {
        float duration = 2f;
        float targetSize = 14f;
        float startSize = Camera.main.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = targetSize;
    }

    public void GenerateWalls(int blocksPerRoom, float blockSize)
    {
        Debug.Log("generate walls");
        Vector3 topLeftCorner = new Vector3(
                transform.position.x - (0.5f * blocksPerRoom - 0.5f) * blockSize, 
                transform.position.y + (0.5f * blocksPerRoom - 0.5f) * blockSize, 0f);
        Vector3 botLeftCorner = new Vector3(
                transform.position.x - (0.5f * blocksPerRoom - 0.5f) * blockSize, 
                transform.position.y - (0.5f * blocksPerRoom - 0.5f) * blockSize, 0f);
        Vector3 botRightCorner = new Vector3(
                transform.position.x + (0.5f * blocksPerRoom - 0.5f) * blockSize, 
                transform.position.y - (0.5f * blocksPerRoom - 0.5f) * blockSize, 0f);
        
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
}
