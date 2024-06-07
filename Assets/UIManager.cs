using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private GameObject defeatScreen;
    void Start()
    {
        Health.OnPlayerDeath += HandleOnPlayerDeath;
    }

    void HandleOnPlayerDeath()
    {
        ShowDefeatScreen();
    }

    void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
}
