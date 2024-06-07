using UnityEngine;

public class CXPOrb : Collectables
{
    [SerializeField] private int xpGain = 20;
    
    protected override void Pick()
    {
        AddXP();
    }

    private void AddXP()
    {
        //Debug.Log($"{xpGain} xp gained!");
        characterStats.experience += xpGain;
    }
}