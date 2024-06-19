using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedSign : MonoBehaviour
{

    public AUpgrade aUpgrade;

    GameManager gameManager;
  
    private void Start() {

        gameManager = GameManager.Ins;
    }
    public void OpenUnlockPenel() {

        if(aUpgrade is ObjectUpgrade) {
           
            string curSceneName = gameManager.sceneManager.sceneChanger.currentScene.name;
            string objName = aUpgrade.gameObject.name;
            if (objName.Contains("Table")) objName = "Table";
         
            if (((ObjectUpgrade)aUpgrade).isGlobal) curSceneName = "Global";

            ItemDatabase itemData = gameManager.database.itemList.items[curSceneName][objName][0];
            gameManager.uiManager.panelUIManager.ShowUnlockSign(itemData, aUpgrade);
        } else if(aUpgrade is Plot) {
             gameManager.uiManager.panelUIManager.ShowUnlockPlot(aUpgrade);
        }

       
    }
}
