using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class MiniMapController : MonoBehaviour
{
    public List<ObjectMap> maps;
    [HideInInspector]
    public ObjectMap currentMap;
    Dictionary<string, LocationDatabase> locationDatabase;
    SceneGameManager sceneManager;
    SaveManager save;
    GameManager gameManager;
    void Start() {
      
    }
    private void OnEnable() {
        sceneManager = GameManager.Ins.sceneManager;
        save = SaveManager.Ins;
        gameManager = gameManager = GameManager.Ins;
        ShowMap();
    }

    public void ShowMap() {
       
        if (sceneManager == null) sceneManager = GameManager.Ins.sceneManager;
        if (gameManager == null) gameManager = GameManager.Ins;
        locationDatabase = gameManager.database.Locations;
        for (int i = 0; i < maps.Count; i++) {
            maps[i].unlock = sceneManager.IsSceneUnlocked(maps[i].name);
            maps[i].index = i;
            maps[i].SetupMap();
            maps[i].location = locationDatabase[maps[i].name];
        }
    }

    public void SetOnUnlock() {
        currentMap.unlock = true;
        currentMap.SetupMap();
    }

    public void OnClickUnlock(ObjectMap objectMap) {
        currentMap = objectMap;
        gameManager.uiManager.panelUIManager.ShowUnlockLocation(this);
    }

    public void MoveToScene(int index) {
        sceneManager.sceneChanger.MoveToScene(index);
        ClosePanel();
    }

    public void ClosePanel() {
       
        gameObject.SetActive(false);
    }
}
