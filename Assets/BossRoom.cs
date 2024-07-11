using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private HSVChange bg;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject vfx;

    public void StartBossFight()
    {
        Destroy(vfx);
        Destroy(circle);
        bg.DecreaseColourChange();
        Instantiate(bossPrefab, transform.position + new Vector3(0f, 15f, 0f), Quaternion.identity, transform);
        UIManager.Instance.ShowBossHUD();
        //Health bossHealth = bossPrefab.GetComponent<Health>();
        //bossHealth.CurrentHealth = bossHealth.MaxHealth;
        StartCoroutine(ZoomOutCoroutine());
        LevelManager.Instance.GenerateBossWalls(transform.position, blockPrefab);
    }

    IEnumerator ZoomOutCoroutine()
    {
        float duration = 2f;
        float targetSize = 15f;
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

    
}
