using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private int xpGranted;
    [SerializeField] private int orbCount = 1;
    
    public void DropLoot()
    {
        if (xpPrefab != null) 
        { 
            for (int i = 0; i < orbCount; i++)
            {
                float offsetX = Random.Range(-0.2f*(orbCount-1f), 0.2f*(orbCount-1f));
                float offsetY = Random.Range(-0.2f*(orbCount-1f), 0.2f*(orbCount-1f));
                Vector3 randOffset = new Vector3(offsetX,offsetY, 0f);
                GameObject xpGO = Instantiate(xpPrefab, transform.position + randOffset , Quaternion.identity); 
                xpGO.GetComponent<CXPOrb>().xpGranted = xpGranted;
            }
        }
    }
}
