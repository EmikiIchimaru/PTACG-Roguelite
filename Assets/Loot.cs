using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject xp;
    
    public void DropLoot()
    {
        if (xp != null) { Instantiate(xp, transform.position, Quaternion.identity); }
    }
}
