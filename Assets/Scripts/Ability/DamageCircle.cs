using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    [HideInInspector] public Character character;
    [HideInInspector] public float damage;
    private float radiusX = 2.25f;
    private float interval = 0.2f;
    void Start()
    {
        InvokeRepeating("DamageCircleRepeating", interval, interval);
    }

    private void DamageCircleRepeating()
    {
        AbilityCreator.AreaDamage(character, transform.position, damage * interval, radiusX * transform.localScale.x);
    }
}
