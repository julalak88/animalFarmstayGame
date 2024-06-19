using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livestock : ObjectUpgrade
{

    public int maxFood = 5;

    //[HideInInspector]
    public LivestockData data;
    //[HideInInspector]
    //public GrassManager grassManager;

    GameObject feedIcon;
    HarvestManager harvest;
    LivestockDatabase database;

    protected override void Awake() {
        base.Awake();
        Transform ico = transform.Find("FeedIcon");
        if (ico) {
            feedIcon = ico.gameObject;
            feedIcon.SetActive(false);
        }
    }

    protected override void Start() {
        base.Start();
        harvest = GameManager.Ins.sceneManager.harvestManager;
    }

    public Vector3 FeedPosition {
        get { return feedIcon.transform.position; }
    }

    public override void LoadObject(string sceneName, string objName, bool _new) {
        base.LoadObject(sceneName, objName, _new);
        database = itemDatabase as LivestockDatabase;
        if (_new) {
            //data.stampTime = DateTime.Now;
            //if(data.numProduct == 0) data.numProduct = database.numProduct;
        }
    }

    public void CheckHungry() {
        if (!_available || feedIcon.activeInHierarchy) return;
        TimeSpan difTime = DateTime.Now - data.stampTime;
        float min = (difTime.TotalMinutes > 30f) ? 30f : (float)difTime.TotalMinutes;
        int i = Mathf.FloorToInt(min / database.eatTime);
        if(i > 0) {
            data.food -= i;
            data.stampTime = DateTime.Now;
        }
        if(data.food <= 0) {
            data.food = 0;
            data.numProduct = 0;
            feedIcon.SetActive(true);
        }else if (data.numProduct == 0) data.numProduct = database.numProduct;
    }

    public bool Gather(Character customer, string productName) {
        if (data.numProduct > 0) {
            harvest.CreateProduct(productName, customer, transform.position, coin, exp,0);
            data.numProduct--;
            return false;
        } else return true;
    }

    public bool isFoodFull() { return (data.food == maxFood); }

    public void Feed() {
        //if (grassManager.CheckGrassEnough(grassConsumption)) {
        //data.stampTime = DateTime.Now;
        //data.numProduct = database.numProduct;
        if(data.numProduct == 0) data.numProduct = database.numProduct;
        feedIcon.SetActive(false);
        //}
    }
}
