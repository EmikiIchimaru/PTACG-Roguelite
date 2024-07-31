using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [SerializeField] private Color colour;
    public void DestroyWithFX()
    {
        Debug.Log("asdf");
        VFXManager.Instance.SpellHit(transform.position, transform.localScale.x, colour);
        //VFXManager.Instance.DeathVFX(transform.position, transform.localScale.x);
        Destroy(gameObject);
    }   
    
}
