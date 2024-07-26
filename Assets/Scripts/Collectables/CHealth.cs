using UnityEngine;

public class CHealth : Collectables
{
    public float hpHealed;

    void Start()
    {
        float scale = 0.5f + hpHealed * 0.01f;
        transform.localScale = new Vector3(scale, scale, 1f);
    }
    
    protected override void Pick()
    {
        AddHP();
    }

    private void AddHP()
    {
        //Debug.Log($"{xpGain} xp gained!");
        health.Heal(hpHealed);
    }
}