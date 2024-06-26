using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    private BossRoom bossRoom;

    void Start()
    {
        bossRoom = GetComponentInParent<BossRoom>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossRoom.StartBossFight();
            Destroy(gameObject);
        }
    }
}
