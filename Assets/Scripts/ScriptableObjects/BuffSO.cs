using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Library", menuName = "Buff Library")]
public class BuffSO : ScriptableObject
{
    public List<Buff> buffs = new List<Buff>();

}