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

    public void ShowRoom()
    {
        if (!isLoaded)
        {
            InitializeRoom();
        }
        else
        {
            gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void HideRoom()
    {
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
        Debug.Log($"hide room {roomNumber}");
    }

    private void InitializeRoom()
    {
        Debug.Log($"init room {roomNumber}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (other.gameObject.tag != "Player") { return; }
        Debug.Log($"{other.gameObject} entered room {roomNumber}! position({positionIndexX}, {positionIndexY})");
        //objectCollided = other.gameObject;
        //objectCollided.GetComponent<Character>(); 
        LevelManager.Instance.PlayerEnteredRoom(roomNumber, positionIndexX, positionIndexY);
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
