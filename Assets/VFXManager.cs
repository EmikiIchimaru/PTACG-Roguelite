using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    //[SerializeField] private GameObject bulletHitPrefab;

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
}
