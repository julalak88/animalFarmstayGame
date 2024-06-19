using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guesthouse : ObjectUpgrade
{
    public bool isFront = true;
    public bool isClean = true;
    public override void LoadObject(string sceneName, string objName, bool _new) {
        if (item) Destroy(item);
        item = null;

        objectName = objName;
        string path = "Upgrade/" + sceneName + "/" + objName+"_"+((isFront) ? "front" : "side");

        Object obj = Resources.Load(path);
        item = Instantiate(obj, transform) as GameObject;

        if (gameManager == null) gameManager = GameManager.Ins;

        itemDatabase = gameManager.database.items[sceneName][objName];
        item.name = objectName;
        coin = itemDatabase.currency.coin;
        exp = itemDatabase.currency.expReward;
        time = itemDatabase.actionTime;
        //SetTargetPositions(itemDatabase.points);
        isClean = true;
       
        if (_new) {
            gameManager.unlockManager.AddRecordOnlyOne(objName);
            _available = true;
            item.AddComponent<UpgradeFX>().OnComplete = ShowFXComplete;
        }
    }
}
