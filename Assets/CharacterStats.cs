using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int level; //property
    public int experience;

    void Start()
    {
        level = 1;
        experience = 0;
    }
}
