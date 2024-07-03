using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform WingWeapon1;
    [SerializeField] private Transform WingWeapon2;
    private float internalCooldown;

    private Character boss;

    private bool isCasting;

    void Awake()
    {
        boss = GetComponent<Character>();
    }

    void Start()
    {
        isCasting = false;
        internalCooldown = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCasting) { return; } 
        if (internalCooldown > 0) { internalCooldown -= Time.deltaTime; }
        if (internalCooldown <= 0) { ChooseBossAbility(); }
    }

    private void ChooseBossAbility()
    {
        StartCoroutine(BossSpinSpray());
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
            AbilityCreator.ShootSP(boss, WingWeapon1.position, 10f, angle2, bulletPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon1.position, 10f, angle+180f, bulletPrefab);
            AbilityCreator.ShootSP(boss, WingWeapon2.position, 10f, angle1, bulletPrefab);
            //AbilityCreator.ShootSP(boss, WingWeapon2.position, 10f, angle+180f, bulletPrefab);
            yield return new WaitForSeconds(interval);
        }

        //GameManager.isPlayerControlEnabled = true;
        //GameManager.isPlayerMovementEnabled = true;
        isCasting = false;
        internalCooldown = 4f;
    }
}
