using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //public LevelManager levelManager;
    public int roomNumber;

    public int positionIndexX;
    public int positionIndexY;

    public bool isLoaded = false;

    private Character character;
    private GameObject objectCollided;

    void Start()
    {
        HideRoom();
    }

    public void ShowRoom()
    {
        if (!isLoaded)
        {
            InitializeRoom();
        }
        else
        {
            //gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void HideRoom()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.SetActive(false);
        Debug.Log($"hide room {roomNumber}");
    }

    private void InitializeRoom()
    {
        Debug.Log($"init room {roomNumber}");
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void PlayerEnter()
    {
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
