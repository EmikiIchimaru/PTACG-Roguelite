using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffDealer : MonoBehaviour
{
    public BuffSO buffLibrary;
    public BuffType playerBuffType;
    public float duration;
    public float speedMultiplier;
    public float damagePerSecond;

    public void DealBuff(EnemyHealth enemy)
    {
        Buff selectedBuff = FindBuffOfType(buffLibrary.buffs, playerBuffType);
        
        if (enemy.currentBuff != null)
        {
            enemy.currentBuff.RemoveBuff();
        }

        Buff instBuff = Instantiate(selectedBuff, enemy.transform.position, Quaternion.identity, enemy.transform);
        instBuff.SetupBuff(enemy);

        enemy.currentBuff = instBuff;
        
        
    }

    private Buff FindBuffOfType(List<Buff> buffList, BuffType findType)
    {
        return buffList.FirstOrDefault(buff => buff.type == findType);
    }
}
