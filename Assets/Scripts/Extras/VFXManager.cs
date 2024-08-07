using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private ParticleSystem spellHit;
    [SerializeField] private ParticleSystem deathVFX;
    private ObjectPooler Pool;

    protected override void Awake()
    {
        base.Awake();
        Pool = GetComponent<ObjectPooler>();
    }

    public void BulletHit(Transform hitTransform, Color newColor)
    {
        GameObject vfxPooled = Pool.GetObjectFromPool();
        vfxPooled.transform.position = hitTransform.position;
        vfxPooled.transform.localScale = hitTransform.localScale;
        vfxPooled.SetActive(true);

        ParticleSystem vfxPS = vfxPooled.GetComponent<ParticleSystem>();
        var mainModule = vfxPS.main;
        mainModule.startColor = newColor;
    }

    public void SpellHit(Vector3 position, float scale, Color newColor)
    {
        ParticleSystem vfxPS = Instantiate(spellHit, position, Quaternion.identity);
        vfxPS.transform.localScale = new Vector3(scale, scale, 1f);
        var mainModule = vfxPS.main;
        mainModule.startColor = newColor;
    }
    public void DeathVFX(Vector3 position, float scale)
    {
        ParticleSystem vfxPS = Instantiate(deathVFX, position, Quaternion.identity);
        float newScale = 0.75f + 0.25f * scale;
        vfxPS.transform.localScale = new Vector3(newScale, newScale, 1f);
        //var mainModule = vfxPS.main;
        //mainModule.startColor = newColor;
    }
}
