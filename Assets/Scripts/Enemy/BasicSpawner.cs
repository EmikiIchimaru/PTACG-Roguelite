using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{



    private RoomEntity roomEntity;
    public GameObject prefab; // Reference to the prefab to be spawned
    //public Transform spawnLocation; // Location where the prefab will be spawned
    public float spawnInterval = 0.5f; // Time interval between spawns

    private List<GameObject> spawns = new List<GameObject>();
    public int maxChildren = 5;

    void Start()
    {
        roomEntity = GetComponent<RoomEntity>();
        // Start the periodic spawning
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    void SpawnPrefab()
    {
        if (!roomEntity.isPlayerInSameRoom) { return; } // Instantiate the prefab at the specified location with the default rotation
        if (spawns.Count > maxChildren) { return; } 
        GameObject childGO = Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);
        childGO.GetComponent<SpawnedEnemy>().basicSpawner = this;
        RoomEntity childRoomEntity = childGO.GetComponent<RoomEntity>();
        childRoomEntity.positionIndexX = roomEntity.positionIndexX;
        childRoomEntity.positionIndexY = roomEntity.positionIndexY;
        spawns.Add(childGO);
    }

    public void RemoveFromList(GameObject gameObject)
    {
        spawns.Remove(gameObject);
    }
}
