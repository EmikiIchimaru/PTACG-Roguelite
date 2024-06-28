using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected float abilityCooltime;
    //[SerializeField] protected GameObject bulletPrefab; 
    protected float internalCooldown = 0f;
    protected Vector3 mousePosition;
    protected float angle;

    public float percentageCooltime { get { return GetPercentageCooltime(); } }
    public Character AbilityOwner { get; set; }
    public CharacterStats stats { get; set; }

    void Update()
    {
        if (internalCooldown > 0f) { internalCooldown -= (Time.deltaTime * 0.01f * stats.abilityHasteFinal); }
        UIManager.Instance.UpdateAbilityCooltime(percentageCooltime);
    }

    public void RequestAbilityCast(Vector3 inputMousePosition)
    {
        if (internalCooldown > 0f) { return; }

        mousePosition = inputMousePosition;
        Vector3 direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Debug.Log("cast ability");

        CastAbility();
        
        //
        internalCooldown = abilityCooltime;
    }

    protected virtual void CastAbility()
    {

    }

    public void SetOwner(Character owner)
    {
        AbilityOwner = owner; 
        //WeaponOwnerLook = owner?.GetComponent<CharacterLook>();
        stats = owner.GetComponent<CharacterStats>();
        //controller = WeaponOwner.GetComponent<PCController>();
    }
    private float GetPercentageCooltime()
    {
        return (1f - (internalCooldown/abilityCooltime));
    }
}
