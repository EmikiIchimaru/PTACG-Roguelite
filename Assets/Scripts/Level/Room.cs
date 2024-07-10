using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    //public LevelManager levelManager;
    public static bool isNextRoomBossRoom = false;
    public RoomPrefabSO roomPool;
    //[SerializeField] private RoomContent bossRoomPrefab;
    
    public int roomNumber;
    public int positionIndexX;
    public int positionIndexY;
    

    public RoomContent roomContent = null;
    private int roomContentIndex;
    private int difficultyLevel;

    

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
            roomContent.gameObject.SetActive(true);
        }
    }

    public void HideRoom()
    {
        if (roomContent == null) { return; }
        roomContent.gameObject.SetActive(false);
    }

    public void ReplaceWithBossRoom()
    {
        //Destroy(roomContent.gameObject);
        //Instantiate(bossRoomPrefab, transform.position, Quaternion.identity, transform);
        isNextRoomBossRoom = true;
    }

    public void SetDifficultyLevel(int x, int y)
    {
        int dX = Mathf.Abs(x-LevelManager.Instance.currentX);
        int dY = Mathf.Abs(y-LevelManager.Instance.currentY);
        difficultyLevel = Mathf.Max(dX, dY);
    }

    public void PlayerEnter()
    {
        //Debug.Log($"player enter {positionIndexX}, {positionIndexY}");
        LevelManager.Instance.PlayerEnteredRoom(positionIndexX, positionIndexY);
    }

    private void InitializeRoom()
    {
        roomContent = Instantiate(GetRoomContent(), transform.position, Quaternion.identity, transform);

        if (isNextRoomBossRoom)
        {
            isNextRoomBossRoom = false;
            GameManager.Instance.CameraBossRoom(transform);
        }

        foreach (Transform child in roomContent.transform)
        {
            RoomEntity entity = child.GetComponent<RoomEntity>();
            if (entity != null)
            {
                entity.room = this;
                entity.positionIndexX = positionIndexX;
                entity.positionIndexY = positionIndexY;
            }
        }
    }

    private RoomContent GetRoomContent()
    {
        if (isNextRoomBossRoom)
        {
            return roomPool.bossRoom;
        }

        if (positionIndexX == LevelManager.Instance.startX && positionIndexY == LevelManager.Instance.startY)
        {
            return roomPool.tutorialRoom;
        }

        List<RoomContent> tempList = new List<RoomContent>();
        tempList.AddRange(roomPool.roomContents);

        tempList = tempList.Where(roomContent => roomContent.difficultyLevel == difficultyLevel).ToList(); 

        if (tempList.Count > 0)
        { 
            return tempList.RandomItem();
        } 
        else
        {
            //roomPool.roomContents.Count;
            return roomPool.GetRandomMaxRoomContent(); //roomPool.roomContents.Count];
        }
        //Random.Range(1,roomPool.roomPrefabs.Length);
        
        
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
