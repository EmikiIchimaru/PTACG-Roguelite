using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffDealer : MonoBehaviour
{

    public BuffSO buffLibrary;
    public BuffType playerBuffType;

   public float procChance = 0.2f;
    //public float durationMultiplier = 1f; 
    
    private CharacterStats stats;
    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void DealBuff(EnemyHealth enemy)
    {
        if (Random.Range(0f,1f) > procChance) { return; }

        Buff selectedBuff = FindBuffOfType(buffLibrary.buffs, playerBuffType);
        
        if (enemy.currentBuff != null)
        {
            if (enemy.currentBuff.type == playerBuffType)
            {
                enemy.currentBuff.duration = selectedBuff.duration;
                return;
            }
            else
            {
                enemy.currentBuff.RemoveBuff();
            }
        }

        Buff instBuff = Instantiate(selectedBuff, enemy.transform.position, Quaternion.identity, enemy.transform);
        instBuff.damagePerTick *= (1 + 0.02f * stats.abilityPowerFinal);
        //instBuff.transform.localScale = enemy.transform.localScale;
        instBuff.SetupBuff(enemy);
        enemy.currentBuff = instBuff;
        
    }

    private Buff FindBuffOfType(List<Buff> buffList, BuffType findType)
    {
        return buffList.FirstOrDefault(buff => buff.type == findType);
    }
}
