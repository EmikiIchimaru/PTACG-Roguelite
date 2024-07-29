using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOrbit : Ability
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float duration;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private GameObject starPrefab;

    protected override void CastAbility()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject starGO = Instantiate(starPrefab, AbilityOwner.transform.position, Quaternion.identity);
            starGO.transform.Rotate(0,0,72*i);

            Projectile projectile = starGO.GetComponentInChildren<Projectile>();
            projectile.ProjectileOwner = AbilityOwner;
            projectile.buffDealer = AbilityOwner?.GetComponent<BuffDealer>();
            projectile.damage = baseDamage * (1 + 0.05f * stats.abilityPowerFinal);

            /* if (AbilityOwner.CharacterType == Character.CharacterTypes.Player)
            {
                SpriteRenderer sr = projectile.GetComponent<SpriteRenderer>();
                Color projectileColor = Colour.ElementToColour(UpgradeManager.Instance.playerAbilityElement);
                sr.color = projectileColor;
            } */


            FollowParent orbit = starGO.GetComponent<FollowParent>();
            orbit.parent = transform;
            orbit.speed = orbitSpeed;
            starGO.GetComponent<TimedLife>().lifetime = duration;
        }
    }
}
