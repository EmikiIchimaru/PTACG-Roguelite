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
    private int roomContentIndex;

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
        roomContentIndex = GetRoomContentIndex();
        GameObject prefab = roomPool.roomPrefabs[roomContentIndex];
        roomContent = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        foreach (Transform child in roomContent.transform)
        {
            RoomEntity entity = child.GetComponent<RoomEntity>();
            if (entity != null)
            {
                entity.positionIndexX = positionIndexX;
                entity.positionIndexY = positionIndexY;
            }
        }
    }

    public void PlayerEnter()
    {
        //Debug.Log($"player enter {positionIndexX}, {positionIndexY}");
        LevelManager.Instance.PlayerEnteredRoom(positionIndexX, positionIndexY);
    }

    private int GetRoomContentIndex()
    {
        if (positionIndexX == LevelManager.Instance.currentX && positionIndexY == LevelManager.Instance.currentY)
        {
            return 0;
        }
        else
        {
            return Random.Range(1,roomPool.roomPrefabs.Length);
        }
        
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
