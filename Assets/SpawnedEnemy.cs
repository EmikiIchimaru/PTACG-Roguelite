using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedEnemy : MonoBehaviour
{
    public BasicSpawner basicSpawner { private get; set; }
    void OnDestroy()
    {
        if (basicSpawner != null)
        {
            basicSpawner.RemoveFromList(gameObject);
        }
    }
}
