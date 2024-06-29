using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private float acquisitionRange = 40f;

    public bool isPlayerInRange;
    public Vector3 direction;// { get; set; }
    public float angle { get; set; }
    //public float angleTowardsPlayer;
    private RoomEntity roomEntity;
    // Start is called before the first frame update
    void Start()
    {
        roomEntity = GetComponent<RoomEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = GameManager.Instance.playerCharacter.transform.position.x - transform.position.x;
        float dy = GameManager.Instance.playerCharacter.transform.position.y - transform.position.y;
        isPlayerInRange = (roomEntity.isPlayerInSameRoom && (acquisitionRange * acquisitionRange) > (dx * dx + dy * dy));

        direction = GameManager.Instance.playerCharacter.transform.position - transform.position;
        
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
