using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weaponToUse;

    private DetectPlayer detectPlayer;

    private EnemyMovement enemyMovement;

    private Weapon weapon;

    void Awake()
    {
        detectPlayer = GetComponent<DetectPlayer>();
        enemyMovement = GetComponent<EnemyMovement>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        weapon = Instantiate(weaponToUse, transform.position, transform.rotation);
        weapon.transform.Rotate(0,0,-90f);
        //weapon.transform.localScale = new Vector3(stats.scaleFinal,stats.scaleFinal,1f);
        weapon.transform.parent = enemyMovement.rotatePart;
        
        weapon.SetOwner(GetComponent<Character>());
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer.isPlayerInRange) { weapon.UseWeapon(); }
    }
}
