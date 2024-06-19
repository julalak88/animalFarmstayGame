using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public MoneyController money;
    public MenuController menu;
    public PanelUIManager panelUIManager;
    public DialogManager dialog;
    public AlertPopup alertPopup;
    public GameObject loading;
    SceneGameManager sceneManager;
    GameManager gm;
    private void Start() {
        gm = GameManager.Ins;
        sceneManager = gm.sceneManager;
    }


    public void ShowUpgradeIconInCurrentScene() {
        if(gm == null) gm = GameManager.Ins;
        if(sceneManager == null) sceneManager = GameManager.Ins.sceneManager;
        menu.cancelUpgrade.SetActive(true);
        menu.allMenu.SetActive(false);
        sceneManager.sceneChanger.enable = false;
 
        sceneManager.sceneChanger.currentScene.ShowUpgradeIcon(true);
    }

    public void HideUpgradeIconInCurrentScene() {
        if (sceneManager == null) sceneManager = GameManager.Ins.sceneManager;
        menu.allMenu.SetActive(true);
        menu.cancelUpgrade.SetActive(false);
        sceneManager.sceneChanger.enable = true;
        sceneManager.sceneChanger.currentScene.ShowUpgradeIcon(false);
    }
}
