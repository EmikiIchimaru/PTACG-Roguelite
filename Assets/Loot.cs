using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject xpPrefab;
    [SerializeField] private int xpGranted;
    
    public void DropLoot()
    {
        if (xpPrefab != null) 
        { 
            GameObject xpGO = Instantiate(xpPrefab, transform.position, Quaternion.identity); 
            xpGO.GetComponent<CXPOrb>().xpGranted = xpGranted;
        }
    }
}
