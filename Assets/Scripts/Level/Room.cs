using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //public LevelManager levelManager;

    public RoomPrefabSO roomPool;
    public int roomNumber;

    public int positionIndexX;
    public int positionIndexY;

    public GameObject roomContent = null;

    //public bool isLoaded = false;

    private Character character;
    private GameObject objectCollided;

    public void ShowRoom()
    {
        if (roomContent == null) 
        {
            InitializeRoom();
        }
        else
        {
            roomContent.SetActive(true);
        }
    }

    public void HideRoom()
    {
        if (roomContent == null) { return; }
        roomContent.SetActive(false);
    }
    

    private void InitializeRoom()
    {
        GameObject prefab = roomPool.roomPrefabs[Random.Range(0,roomPool.roomPrefabs.Length)];
        roomContent = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        foreach (Transform child in roomContent.transform)
        {
            BasicSpawner enemy = child.GetComponent<BasicSpawner>();
            if (enemy != null)
            {
                enemy.positionIndexX = positionIndexX;
                enemy.positionIndexY = positionIndexY;
            }
        }
    }

    public void PlayerEnter()
    {
        //Debug.Log($"player enter {positionIndexX}, {positionIndexY}");
        LevelManager.Instance.PlayerEnteredRoom(positionIndexX, positionIndexY);
    }

/*     private bool IsPlayer()
    {
        character = 
        if (character == null)
        {
            return false;
        }
        return character.CharacterType == Character.CharacterTypes.Player;
	} */
}
