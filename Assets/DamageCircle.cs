using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    [HideInInspector] public Character character;
    [HideInInspector] public float damage;
    private float radius = 2.25f;
    void Update()
    {
        AbilityCreator.AreaDamage(character, transform.position, damage * Time.deltaTime , radius);
    }
}
