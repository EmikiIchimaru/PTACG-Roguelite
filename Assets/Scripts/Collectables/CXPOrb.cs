using UnityEngine;

public class CXPOrb : Collectables
{
    public int xpGranted;

    void Start()
    {
        float scale = xpGranted * 0.2f;
        transform.localScale = new Vector3(scale, scale, 1f);
    }
    
    protected override void Pick()
    {
        AddXP();
    }

    private void AddXP()
    {
        //Debug.Log($"{xpGain} xp gained!");
        characterStats.AddExperience(xpGranted);
    }
}