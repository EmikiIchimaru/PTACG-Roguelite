using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public LevelGenerator levelGen;

    public int positionIndexX;
    public int positionIndexY;

    public bool isLoaded = false;

    private Character character;
    private GameObject objectCollided;

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectCollided = other.gameObject;
        if (IsPlayer())
        {
            Debug.Log("player entered room");
            //levelGen.LoadWalls(positionIndexX,pxy
        }
	} 

    private bool IsPlayer()
    {
        character = objectCollided.GetComponent<Character>();
        if (character == null)
        {
            return false;
        }
        return character.CharacterType == Character.CharacterTypes.Player;
	}
}
