using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{   
    public static Action OnStartShooting;
	
    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    //[SerializeField] private Transform weaponHolderPosition;

    // Reference of the Weapon we are using
    public Weapon CurrentWeapon  { get; set; }

    // Store the reference to the second weapon
    //public Weapon SecondaryWeapon { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse);
    }

    protected override void HandleInput()
    {
        if (!GameManager.isPlayerControlEnabled) { return; }
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (Input.GetMouseButton(0) || GameManager.isAutoShoot)
            {
                Shoot();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopWeapon();    
            }
        

            /* if (Input.GetKeyDown(KeyCode.Alpha1) && SecondaryWeapon != null)
            {
                //EquipWeapon(weaponToUse, weaponHolderPosition);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
            {
                EquipWeapon(SecondaryWeapon, weaponHolderPosition);
            } */
        }   
    }
    
    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        } 

        CurrentWeapon.UseWeapon();
        if (character.CharacterType == Character.CharacterTypes.Player)
        {	
            OnStartShooting?.Invoke();
            //UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }     
    }

    // When we stop shooting we stop using our Weapon
    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.StopWeapon();
    }


    public void EquipWeapon(Weapon weapon)
    {

        if (CurrentWeapon != null)
        {
            ProjectileWeapon projectileWeapon = (ProjectileWeapon) CurrentWeapon;
            //WeaponAim.DestroyReticle();       // Each weapon has its own Reticle component
            Destroy(projectileWeapon.Pool);
            Destroy(CurrentWeapon.gameObject);
        }

        CurrentWeapon = Instantiate(weapon, transform.position, transform.rotation);
        CurrentWeapon.transform.localScale = new Vector3(stats.scaleFinal, stats.scaleFinal, 1f);
        CurrentWeapon.transform.parent = transform;
        CurrentWeapon.SetOwner(character);     
        
        foreach (Transform t in CurrentWeapon.transform)
        {
            ProjectileWeapon subWeapon = t.GetComponent<ProjectileWeapon>();
            if (subWeapon != null) 
            { 
                subWeapon.SetOwner(character); 
                CurrentWeapon.SetChildWeapon(subWeapon);
            }
        }

        /* if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);                
            UIManager.Instance.UpdateWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        } */
    } 
}