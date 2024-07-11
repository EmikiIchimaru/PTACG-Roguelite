using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform WingWeapon1;
    [SerializeField] private Transform WingWeapon2;
    private float internalCooldown;

    private float maxInternalCD;

    private Character boss;
    private EnemyMovement enemyMovement;
    private Health health;

    private bool isCasting;

    private bool hasRaged;

    void Awake()
    {
        boss = GetComponent<Character>();
        enemyMovement = GetComponent<EnemyMovement>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        hasRaged = false;
        isCasting = false;
        internalCooldown = 2f;
        maxInternalCD = 1.5f;
    }

    void FixedUpdate()
    {
        enemyMovement.SetPositiveYMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.CurrentHealth < 10000f && !hasRaged)
        {
            hasRaged = true;
            maxInternalCD = 0.75f;
        }
        if (!isCasting)
        {
            if (internalCooldown > 0) { internalCooldown -= Time.deltaTime; }
            if (internalCooldown <= 0) { ChooseBossAbility(); }
        } 
        
    }

    private void ChooseBossAbility()
    {
        int abilityIndex = Random.Range(0,5);
        switch (abilityIndex)
        {
            case 0:
                StartCoroutine(BossSpinSpray());
                break;
            case 1:
                StartCoroutine(BossShootPlayer());
                break;
            case 2:
                StartCoroutine(BossSpawnArrows());
                break;
            case 3:
                StartCoroutine(BossSpawnArrows());
                break;
            case 4:
                StartCoroutine(BossSpawnArrows());
                break;
        }
        
    }
    private IEnumerator BossSpinSpray()
    {
        isCasting = true;
        //GameManager.isPlayerControlEnabled = false;
        //GameManager.isPlayerMovementEnabled = false;

        float interval = 0.05f;

        for (int i = 0; i < 72; i++)
        {
            float angle1 = i * 20f;
            float angle2 = 180f - i * 20f;
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, WingWeapon1.position, 10f, angle2, bombPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon1.position, 10f, angle+180f, bulletPrefab);
            AbilityCreator.ShootSP(boss, WingWeapon2.position, 10f, angle1, bombPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon2.position, 10f, angle+180f, bulletPrefab);
            yield return new WaitForSeconds(interval);
        }

        //GameManager.isPlayerControlEnabled = true;
        //GameManager.isPlayerMovementEnabled = true;
        isCasting = false;
        internalCooldown = maxInternalCD;
    }
    private IEnumerator BossShootPlayer()
    {
        isCasting = true;
        //GameManager.isPlayerControlEnabled = false;
        //GameManager.isPlayerMovementEnabled = false;

        float interval = 0.1f;

        for (int i = 0; i < 25; i++)
        {
            float angle = Utility.GetAngleBetweenPoints(transform.position, GameManager.Instance.playerCharacter.transform.position);
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, transform.position, 10f, angle + Random.Range(-10f,10f), bulletPrefab);
            yield return new WaitForSeconds(interval);
        }

        //GameManager.isPlayerControlEnabled = true;
        //GameManager.isPlayerMovementEnabled = true;
        isCasting = false;
        internalCooldown = maxInternalCD;
    }
    private IEnumerator BossSpawnArrows()
    {
        isCasting = true;
        //GameManager.isPlayerControlEnabled = false;
        //GameManager.isPlayerMovementEnabled = false;
        bool isLeftorRight = true;
        float interval = 0.1f;

        for (int i = 0; i < 20; i++)
        {
            isLeftorRight = !isLeftorRight;
            Vector3 wingPosition = (isLeftorRight) ? WingWeapon1.position : WingWeapon2.position;
            Instantiate(arrowPrefab, wingPosition, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }

        //GameManager.isPlayerControlEnabled = true;
        //GameManager.isPlayerMovementEnabled = true;
        isCasting = false;
        internalCooldown = maxInternalCD;
    }
}
