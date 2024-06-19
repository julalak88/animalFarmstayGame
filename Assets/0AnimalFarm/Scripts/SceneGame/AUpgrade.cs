using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUpgrade : MonoBehaviour
{
    public string objectName;
    public string headerKey;
    public int coin;
    public float exp;
    public float vegExp;
    public float time;

    internal GameManager gameManager;
    internal GameObject item;

    public bool isAvailable {
        get { return _available; }
    }
    internal bool _available = true;

    public void Locked() {
        _available = false;
        if (this is Plot) {
            string objName = "sign1";
            Object obj = Resources.Load("Upgrade/Global/" + objName);
            item = Instantiate(obj, transform) as GameObject;
            item.GetComponent<LockedSign>().aUpgrade = this;
        }
    }
}
