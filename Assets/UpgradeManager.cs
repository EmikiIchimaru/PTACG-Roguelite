using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private GameObject upgradeCanvas;

    
    public void ShowCanvas()
    {
        upgradeCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void SelectUpgrade1()
    {
        Debug.Log("upgrade 1");
        HideCanvas();
    }
    
    public void SelectUpgrade2()
    {
        Debug.Log("upgrade 2");
        HideCanvas();
    }
    
    public void SelectUpgrade3()
    {
        Debug.Log("upgrade 3");
        HideCanvas();
    }
    

}
