using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject kitePrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject shooterPrefab;
    [SerializeField] private GameObject shieldPrefab;

    [SerializeField] private Transform WingWeapon1;
    [SerializeField] private Transform WingWeapon2;
    [SerializeField] private Transform OuterWing1;
    [SerializeField] private Transform OuterWing2;
    private float internalCooldown;
    private float maxInternalCD;

    private Character boss;
    private EnemyMovement enemyMovement;
    private Health health;

    private int rage;

    public static List<GameObject> spawns = new List<GameObject>();

    void Awake()
    {
        boss = GetComponent<Character>();
        enemyMovement = GetComponent<EnemyMovement>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        rage = 0;
        internalCooldown = 2f;
        maxInternalCD = 2.5f;
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
            maxInternalCD = 1.5f;
            internalCooldown = 10f;
            ShieldBoss(10f);
            StartCoroutine(BossSpawnShooters(OuterWing1, 4));
            StartCoroutine(BossSpawnShooters(OuterWing2, 4));
            //abilityPool = 4;
        }

        if (health.GetPercentHP() < 0.3f && rage == 1)
        {
            rage = 2;
            maxInternalCD = 0.5f;
            internalCooldown = 10f;
            ShieldBoss(10f);
            StartCoroutine(BossSpawnShooters(OuterWing1, 7));
            StartCoroutine(BossSpawnShooters(OuterWing2, 7));
            //abilityPool = 5;
        }
        
        if (internalCooldown > 0) { internalCooldown -= Time.deltaTime; }
        if (internalCooldown <= 0) 
        {
            ChooseBossAbility(); 
            
        }
    
        
    }

    private void ChooseBossAbility()
    {
        int abilityIndex = Random.Range(0,5);
        switch (abilityIndex)
        {
            case 0:
                StartCoroutine(BossSpinSpray());
                internalCooldown = maxInternalCD + 3f;
                break;
            case 1:
                StartCoroutine(BossShootPlayer(WingWeapon1, 30, 0.1f, 5f));
                StartCoroutine(BossShootPlayer(WingWeapon2, 30, 0.1f, 5f));
                internalCooldown = maxInternalCD + 3f;
                break;
            case 2:
                StartCoroutine(BossSpawnArrows(OuterWing1));
                StartCoroutine(BossSpawnArrows(OuterWing2));
                internalCooldown = maxInternalCD + 2f;
                break;
            case 3:
                StartCoroutine(BossRing());
                StartCoroutine(BossBombPlayer(WingWeapon1, 20, 0.3f, 30f));
                StartCoroutine(BossBombPlayer(WingWeapon2, 20, 0.3f, 30f));
                internalCooldown = maxInternalCD + 6f;
                break;
            case 4:
                StartCoroutine(BossMachineGun());
                internalCooldown = maxInternalCD + 4f;
                break;
        }
        
    }
    private IEnumerator BossSpinSpray()
    {

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

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Utility.GetAngleBetweenPoints(newTransform.position, GameManager.Instance.playerCharacter.transform.position);
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, newTransform.position, 15f, angle + Random.Range(-spray,spray), bulletPrefab);
            yield return new WaitForSeconds(interval);
        }


    }

    private IEnumerator BossBombPlayer(Transform newTransform, int bulletCount, float interval, float spray)
    {
        //float interval = 0.1f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Utility.GetAngleBetweenPoints(newTransform.position, GameManager.Instance.playerCharacter.transform.position);
            //Debug.Log(i);
            AbilityCreator.ShootSP(boss, newTransform.position, 15f, angle + Random.Range(-spray,spray), bombPrefab);
            yield return new WaitForSeconds(interval);
        }


    }

    private IEnumerator BossSpawnArrows(Transform newTransform)
    {
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

    private IEnumerator BossRing()
    {

        float interval = 0.75f;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                float angleP = j * 30f;
                Vector2 offset2D = Utility.RotateVector(new Vector2(3,0), angleP);
                Vector3 transformOffset = transform.position + new Vector3(offset2D.x, offset2D.y, 0);
                float angle = Utility.GetAngleBetweenPoints(transformOffset, GameManager.Instance.playerCharacter.transform.position);
                AbilityCreator.ShootSP(boss, transformOffset, 10f, angle, ringPrefab);
            }
            float extraInterval = (i == 1 || i == 3 ) ? interval * 2f : interval;  
            yield return new WaitForSeconds(extraInterval);
        }

    }

    private IEnumerator BossMachineGun()
    {
        float interval = 0.04f;
        float angle = Utility.GetAngleBetweenPoints(transform.position, GameManager.Instance.playerCharacter.transform.position);
        //Debug.Log(angle);
        for (int i = 0; i < 100; i++)
        {
            
            //float angle1 = Utility.GetAngleBetweenPoints(OuterWing1.position, GameManager.Instance.playerCharacter.transform.position);
            //float angle2 = Utility.GetAngleBetweenPoints(OuterWing2.position, GameManager.Instance.playerCharacter.transform.position);
            //Debug.Log(i);
            float angle3 = Mathf.Abs(50-i) - 20f;
            AbilityCreator.ShootSP(boss, OuterWing1.position, 10f, 0.5f * (-90f+angle) - angle3, kitePrefab);
            AbilityCreator.ShootSP(boss, OuterWing2.position, 10f, 0.5f * (-90f+angle) + angle3, kitePrefab);
            yield return new WaitForSeconds(interval);
        }
    }

    private void ShieldBoss(float time)
    {
        GameObject shieldGO = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shieldGO.transform.parent = transform;
        shieldGO.GetComponent<TimedLife>().lifetime = time;
    }
}
