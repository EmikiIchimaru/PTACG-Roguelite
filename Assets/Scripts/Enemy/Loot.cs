using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private int amount;
    [SerializeField] private int orbCount = 1;
    
    public void DropLoot()
    {
        if (xpPrefab != null) 
        { 
            for (int i = 0; i < orbCount; i++)
            {
                float offsetX = Random.Range(-0.5f*(orbCount-1f), 0.5f*(orbCount-1f));
                float offsetY = Random.Range(-0.5f*(orbCount-1f), 0.5f*(orbCount-1f));
                Vector3 randOffset = new Vector3(offsetX,offsetY, 0f);
                GameObject lootGO = Instantiate(xpPrefab, transform.position + randOffset , Quaternion.identity); 

                CXPOrb cxp = lootGO.GetComponent<CXPOrb>();
                if (cxp != null) { cxp.xpGranted = amount; }

                CHealth ch = lootGO.GetComponent<CHealth>();
                if (ch != null) { ch.hpHealed = amount; }
            }
        }
    }
}
