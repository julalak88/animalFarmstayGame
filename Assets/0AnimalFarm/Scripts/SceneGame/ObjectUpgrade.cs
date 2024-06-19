using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening.Core.Easing;

public class ObjectUpgrade : AUpgrade
{
    public bool isGlobal = false;
    
    public List<PointTarget> targets;

    SceneChanger sceneChanger;
    GameObject upgradeIcon;
    internal ItemDatabase itemDatabase;
    bool unlock;
    public bool showUpgradeIcon {
        set {
            if(upgradeIcon) upgradeIcon.SetActive(value);
        }
        get {
            return (upgradeIcon) ? upgradeIcon.activeInHierarchy : false;
        }
    }

    protected virtual void Awake() {

        for (int i = 0; i < targets.Count; i++) {
            targets[i].aUpgrade = this;
        }
     
        Transform ico = transform.Find("UpgradeIcon");
        if (ico) {
            upgradeIcon = ico.gameObject;
            upgradeIcon.SetActive(false);
        }

        headerKey = gameObject.name;
    }

    protected virtual void Start() {
        gameManager = GameManager.Ins;
        sceneChanger = gameManager.sceneManager.sceneChanger;
    }

    public virtual void LoadObject(string sceneName, string objName) {
       
        LoadObject(sceneName, objName, false);
    }

    public virtual void LoadObject(string sceneName, string objName , bool _new, bool _unlock) {
        if(sceneChanger == null) sceneChanger = gameManager.sceneManager.sceneChanger;
        sceneChanger.enable = false;
        unlock = _unlock;
        LoadObject(sceneName, objName, _new);
    }

    public virtual void LoadObject(string sceneName, string objName, bool _new) {
    
        if (item) Destroy(item);

        item = null;
        string path = "";
        if (isGlobal) path = "Upgrade/Global/" + objName;
        else path = "Upgrade/" + sceneName + "/" + objName;

        objectName = objName;
       
        Object obj = Resources.Load(path);
        item = Instantiate(obj, transform) as GameObject;

        if (gameManager == null) gameManager = GameManager.Ins;

        if (isGlobal) {
            itemDatabase = gameManager.database.items["Global"][objName];
        } else {
            itemDatabase = gameManager.database.items[sceneName][objName];
        }
        item.name = objName;
        coin = itemDatabase.currency.coin;
        exp = itemDatabase.currency.expReward;
        time = itemDatabase.actionTime;
        SetTargetPositions(itemDatabase.points);
        
        if (_new) {
            gameManager.unlockManager.AddRecordOnlyOne(objName);
            _available = true;
            item.AddComponent<UpgradeFX>().OnComplete = ShowFXComplete;
        }
       
       
    }

    protected void ShowFXComplete() {
        if (unlock) {
            gameManager.unlockManager.CheckForUnlock(itemDatabase);
            unlock = false;
            sceneChanger.enable = true ;
        }
        print("-------------- ShowFXComplete");
    }

    public void SetTargetPositions(List<Vector2> positions) {
        if (positions.Count == 0) return;
        for (int i = 0; i < targets.Count; i++) {
            targets[i].transform.localPosition = positions[i];
        }
    }

    public void ShowUpgradeUI() {
        if (gameManager == null) gameManager = GameManager.Ins;
        gameManager.currentBuilding = transform.position;
        gameManager.uiManager.panelUIManager.ShowUIUpgrade(this);
        
    }

     

}
