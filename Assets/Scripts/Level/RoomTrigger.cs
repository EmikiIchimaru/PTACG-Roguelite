using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //
        if (other.gameObject.tag != "Room") { return; }
        //Debug.Log($"{other.gameObject} entered room {roomNumber}! position({positionIndexX}, {positionIndexY})");
        other.gameObject.GetComponent<Room>().PlayerEnter(); 
	} 
}
