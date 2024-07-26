using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject bluePrefab;
    [SerializeField] private GameObject kitePrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject shooterPrefab;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private GameObject shieldPrefab;

    [SerializeField] private Transform WingWeapon1;
    [SerializeField] private Transform WingWeapon2;
    [SerializeField] private Transform OuterWing1;
    [SerializeField] private Transform OuterWing2;
    [SerializeField] private SpriteRenderer indicator;
    private float internalCooldown;
    private float maxInternalCD;

    
    private Character boss;
    private EnemyMovement enemyMovement;
    private Health health;

    private int rage;
    private Vector3 playerLastSeen;
    private int previousAbility;

    public static List<GameObject> spawns = new List<GameObject>();

    private readonly float defaultInterval = 0.75f;

    void Awake()
    {
        boss = GetComponent<Character>();
        enemyMovement = GetComponent<EnemyMovement>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        rage = 0;
        previousAbility = -1;
        internalCooldown = 2f;
        maxInternalCD = 2f;
        playerLastSeen = GameManager.Instance.playerCharacter.transform.position;
    }

    void FixedUpdate()
    {
        enemyMovement.SetPositiveYMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (health.GetPercentHP() < 0.6f && rage == 0)
        {
            rage = 1;
            maxInternalCD = 1.25f;
            internalCooldown = 12f;
            ShieldBoss(10f);
            StartCoroutine(BossSpawnShooters(OuterWing1, 4));
            StartCoroutine(BossSpawnShooters(OuterWing2, 4));
            //abilityPool = 4;
        }

        if (health.GetPercentHP() < 0.3f && rage == 1)
        {
            rage = 2;
            maxInternalCD = 0.5f;
            internalCooldown = 12f;
            ShieldBoss(10f);
            StartCoroutine(BossSpawnShooters(OuterWing1, 7));
            StartCoroutine(BossSpawnShooters(OuterWing2, 7));
            //abilityPool = 5;
        }
        playerLastSeen = Vector3.MoveTowards(playerLastSeen, GameManager.Instance.playerCharacter.transform.position, 1f);
        if (internalCooldown > 0) { internalCooldown -= Time.deltaTime; }
        if (internalCooldown <= 0) 
        {
            ChooseBossAbility(); 
            
        }
    
        
    }

    private void ChooseBossAbility()
    {
        int abilityIndex = Random.Range(0, 4+rage);
        while (abilityIndex == previousAbility)
        {
            abilityIndex = Random.Range(0, 4+rage);
        }
        previousAbility = abilityIndex;
        Indicate(abilityIndex);
        switch (abilityIndex)
        {
            case 0:
                StartCoroutine(BossSpinSpray());
                internalCooldown = maxInternalCD + 4f;
                break;
            case 1:
                StartCoroutine(BossShootPlayer(WingWeapon1, 30, 0.1f, 5f));
                StartCoroutine(BossShootPlayer(WingWeapon2, 30, 0.1f, 5f));
                internalCooldown = maxInternalCD + 4f;
                break;
            case 2:
                //StartCoroutine(BossSpawnArrows(OuterWing1));
                //StartCoroutine(BossSpawnArrows(OuterWing2));
                StartCoroutine(BossClock());
                internalCooldown = maxInternalCD + 7f;
                break;
            case 3:
                StartCoroutine(BossRing());
                StartCoroutine(BossBombPlayer(WingWeapon1, 20, 0.3f, 30f));
                StartCoroutine(BossBombPlayer(WingWeapon2, 20, 0.3f, 30f));
                internalCooldown = maxInternalCD + 7f;
                break;
            case 4:
                StartCoroutine(BossCircle());
                internalCooldown = maxInternalCD + 5f;
                break;
            case 5:
                Instantiate(orbPrefab, WingWeapon1.position, Quaternion.identity);
                Instantiate(orbPrefab, WingWeapon2.position, Quaternion.identity);
                internalCooldown = maxInternalCD + 5f;
                break;
                
        }
        
    }

    private void Indicate(int abilityIndex)
    {
        Color newColour = Color.white;
        switch (abilityIndex)
        {
            case 0:
                newColour = new Color(1f,0f,0f);
                break;
            case 1:
                newColour = new Color(1f,0.25f,0f);
                break;
            case 2:
                newColour = new Color(1f,0f,0.75f);
                break;
            case 3:
                newColour = new Color(1f,1f,0f);
                break;
            case 4:
                newColour = new Color(0f,0f,1f);
                break;
            case 5:
                newColour = new Color(1f,0f,1f);
                break;
            default:
                break;
        }
        indicator.color = newColour;
    }
    private IEnumerator BossSpinSpray()
    {
        yield return new WaitForSeconds(defaultInterval);

        float interval = 0.05f;

        for (int i = 0; i < 72; i++)
        {
            float angle1 = i * 20f;
            float angle2 = 180f - i * 20f;
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, WingWeapon1.position, 30f, angle2, bombPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon1.position, 10f, angle+180f, bulletPrefab);
            AbilityCreator.ShootSP(boss, WingWeapon2.position, 30f, angle1, bombPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon2.position, 10f, angle+180f, bulletPrefab);
            yield return new WaitForSeconds(interval);
        }

    }
    private IEnumerator BossShootPlayer(Transform newTransform, int bulletCount, float interval, float spray)
    {
        //float interval = 0.1f;
        yield return new WaitForSeconds(defaultInterval);
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Utility.GetAngleBetweenPoints(newTransform.position, playerLastSeen);
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, newTransform.position, 15f, angle + Random.Range(-spray,spray), bulletPrefab);
            yield return new WaitForSeconds(interval);
        }


    }

    private IEnumerator BossBombPlayer(Transform newTransform, int bulletCount, float interval, float spray)
    {
        //float interval = 0.1f;
        yield return new WaitForSeconds(defaultInterval);
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Utility.GetAngleBetweenPoints(newTransform.position, playerLastSeen);
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, newTransform.position, 25f, angle + Random.Range(-spray,spray), bombPrefab);
            yield return new WaitForSeconds(interval);
        }


    }

    private IEnumerator BossSpawnArrows(Transform newTransform)
    {
        yield return new WaitForSeconds(defaultInterval);

        float interval = 0.15f;

        for (int i = 0; i < 15; i++)
        {
            GameObject spawn = Instantiate(arrowPrefab, newTransform.position, Quaternion.identity);
            spawns.Add(spawn);
            yield return new WaitForSeconds(interval);
        }
    }
    private IEnumerator BossSpawnShooters(Transform newTransform, int count)
    {

        float interval = 0.5f;

        for (int i = 0; i < count; i++)
        {
            GameObject spawn = Instantiate(shooterPrefab, newTransform.position, Quaternion.identity);
            spawns.Add(spawn);
            yield return new WaitForSeconds(interval);
        }


    }

    private IEnumerator BossClock()
    {
        yield return new WaitForSeconds(defaultInterval);

        float interval = 0.02f;

        float angle = 90f;

        for (int i = 0; i < 288; i++)
        {
            bool direction = ((i / 36) % 2 == 0);
            angle = (direction) ? angle + 10f : angle - 10f;
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, transform.position, 30f, angle, kitePrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon1.position, 10f, angle+180f, bulletPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon2.position, 30f, angle1, bombPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon2.position, 10f, angle+180f, bulletPrefab);
            yield return new WaitForSeconds(interval);
        }

    }

    private IEnumerator BossRing()
    {
        yield return new WaitForSeconds(defaultInterval);

        float interval = 0.75f;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                float angleP = j * 30f;
                Vector2 offset2D = Utility.RotateVector(new Vector2(3,0), angleP);
                Vector3 transformOffset = transform.position + new Vector3(offset2D.x, offset2D.y, 0);
                float angle = Utility.GetAngleBetweenPoints(transformOffset, playerLastSeen);
                AbilityCreator.ShootSP(boss, transformOffset, 10f, angle, ringPrefab);
            }
            float extraInterval = (i == 1 || i == 3 ) ? interval * 2f : interval;  
            yield return new WaitForSeconds(extraInterval);
        }

    }

    private IEnumerator BossCircle()
    {
        yield return new WaitForSeconds(defaultInterval);

        float interval = 0.75f;

        for (int n = 0; n < 3; n++)
        {
            for (int i = 0; i < 30; i++)
            {
                float angle = i * 12f;
                AbilityCreator.ShootSP(boss, transform.position, 30f, angle, bluePrefab);
                //AbilityCreator.ShootSP(boss, WingWeapon2.position, 30f, angle1, bombPrefab);
            }
            yield return new WaitForSeconds(interval);
            for (int i = 0; i < 30; i++)
            {
                float angle = i * 12f;
                AbilityCreator.ShootSP(boss, WingWeapon1.position, 15f, angle, bulletPrefab);
                AbilityCreator.ShootSP(boss, WingWeapon2.position, 15f, angle, bulletPrefab);
                //AbilityCreator.ShootSP(boss, WingWeapon2.position, 30f, angle1, bombPrefab);
            }
            yield return new WaitForSeconds(interval);
        }


    }

    /* private IEnumerator BossMachineGun()
    {
        float interval = 0.04f;
        float angle = Utility.GetAngleBetweenPoints(transform.position, playerLastSeen);
        //Debug.Log(angle);
        for (int i = 0; i < 100; i++)
        {
            
            //float angle1 = Utility.GetAngleBetweenPoints(OuterWing1.position, playerLastSeen);
            //float angle2 = Utility.GetAngleBetweenPoints(OuterWing2.position, playerLastSeen);
            //Debug.Log(i);
            float angle3 = Mathf.Abs(50-i) - 20f;
            AbilityCreator.ShootSP(boss, OuterWing1.position, 10f, 0.5f * (-90f+angle) - angle3, kitePrefab);
            AbilityCreator.ShootSP(boss, OuterWing2.position, 10f, 0.5f * (-90f+angle) + angle3, kitePrefab);
            yield return new WaitForSeconds(interval);
        }
    } */


    private void ShieldBoss(float time)
    {
        GameObject shieldGO = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shieldGO.transform.parent = transform;
        shieldGO.GetComponent<TimedLife>().lifetime = time;
    }
}
