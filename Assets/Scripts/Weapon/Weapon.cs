using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{ 
    //[Header("Name")] 
    //[SerializeField] private string weaponName = "";
  
    [Header("Settings")] 

    [SerializeField] protected float baseDamage;
    [SerializeField] protected float baseAttackCooltime;

    [SerializeField] protected bool isMagicWeapon = false;

    public float finalAttackCooltime { protected get; set; }

    public float damageX { get { return GetDamageX(); } }

/*     [Header("Weapon")] 
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true; */
/* 
    [Header("Recoil")] 
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 30; */

/*     [Header("Effects")] 
    [SerializeField] private ParticleSystem muzzlePS; */

    // Returns the name of this Weapon
    //public string WeaponName => weaponName;

    // Reference of the Character that controls this Weapon
    public Character WeaponOwner { get; set; }
    public CharacterStats stats { get; set; }
    public BuffDealer buffDealer { get; set; }
    public CharacterLook WeaponOwnerLook { get; set; }

    public DetectPlayer detectPlayer { get; set; }
    protected Vector2 weaponFacing { get { return GetWeaponFacing(); } }

    // Returns if we can shoot now
    public bool CanShoot { get { return internalCooldown <= 0f; } }

    // Internal
    protected float internalCooldown = 0f;

    //protected Animator animator;
    //private readonly int weaponUseParameter = Animator.StringToHash("WeaponUse");

    protected virtual void Awake()
    {     
        //animator = GetComponent<Animator>();
        finalAttackCooltime = baseAttackCooltime;
    }


    protected virtual void Update()
    {
        float aspdScaling = (stats != null) ? 0.01f * stats.attackSpeedFinal : 1f;
        if (internalCooldown > 0f) { internalCooldown -= (Time.deltaTime * aspdScaling); }
    }

    // Trigger our Weapon in order to use it
    public virtual void UseWeapon()
    {
        RequestShot();
    }

    public void StopWeapon()
    {
        /* if (useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        } */
    }


    // Makes our weapon start shooting
    protected virtual void RequestShot()
    {
        if (!CanShoot) { return; }
        
/*      
        if (useRecoil)
        {
            Recoil();
        } */
         
        //animator.SetTrigger(weaponUseParameter);
        //WeaponAmmo.ConsumeAmmo(); 
        //muzzlePS.Play();     
    }

    // Reference the owner of this Weapon
    public void SetOwner(Character owner)
    {
        WeaponOwner = owner; 
        WeaponOwnerLook = owner?.GetComponent<CharacterLook>();
        detectPlayer = owner?.GetComponent<DetectPlayer>();
        stats = owner?.GetComponent<CharacterStats>();
        buffDealer = owner?.GetComponent<BuffDealer>();
        //controller = WeaponOwner.GetComponent<PCController>();
    }

    private Vector2 GetWeaponFacing()
    {
        if ( WeaponOwnerLook != null ) { return WeaponOwnerLook.direction; }
        if ( detectPlayer != null ) { return detectPlayer.direction; }
        return Vector2.zero;
    }

    private float GetDamageX()
    {
        float tempDamageX = 1f;
        if (stats != null)
        {
            if (!isMagicWeapon)
            {
                tempDamageX = stats.attackDamageFinal;
            }
            else
            {
                tempDamageX = 10f * (1 + 0.01f * stats.abilityPowerFinal);
            }
            
        }
        return tempDamageX;
    }
 
}