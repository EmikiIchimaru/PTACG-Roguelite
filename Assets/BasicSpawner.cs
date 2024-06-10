using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{

    public int positionIndexX;
    public int positionIndexY;
    public GameObject prefab; // Reference to the prefab to be spawned
    //public Transform spawnLocation; // Location where the prefab will be spawned
    public float spawnInterval = 0.5f; // Time interval between spawns

    void Start()
    {
        // Start the periodic spawning
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    void SpawnPrefab()
    {
        if (positionIndexX != LevelManager.Instance.currentX || positionIndexY != LevelManager.Instance.currentY) { return; }// Instantiate the prefab at the specified location with the default rotation
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
