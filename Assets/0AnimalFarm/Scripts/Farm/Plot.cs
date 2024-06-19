using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plot : AUpgrade
{
    public PointTarget target;
    public int index = -1;

    HarvestManager harvest;
    GameObject soil;
    GameObject plantIcon;
    GameObject tree;
   
    PlotData data;
    VegDatabase database;

    Plant plant;

    public GameObject timeBar;
    public GameObject timeSpr;
    public GameObject paricle;
    private void Awake() {
        index = -1;
        target.aUpgrade = this;
        Transform ico = transform.Find("PlantIcon");
        if (ico) {
            plantIcon = ico.gameObject;
            plantIcon.SetActive(false);
        }
        paricle.SetActive(false);
        timeBar.SetActive(false);
        timeSpr.transform.localScale = Vector3.one;
    }

    private void Start() {
        gameManager = GameManager.Ins;
        harvest = gameManager.sceneManager.harvestManager;
    }

    public void LoadObject(PlotData plotData) {

        data = plotData;

        UnityEngine.Object obj;
        if (!soil) {
            obj = Resources.Load("Upgrade/Farm/plot");
            soil = Instantiate(obj, transform) as GameObject;
        }

        if (data.type != "") {
            if (gameManager == null) gameManager = GameManager.Ins;
            if (harvest == null) harvest = GameManager.Ins.sceneManager.harvestManager;
            // database = gameManager.database.vegetables[data.type];
            database = gameManager.database.vegetables.Select(v => v).Where(p => p.name.Equals(data.type)).FirstOrDefault();
            coin = database.currency.coin;
            exp = database.currency.expReward;
            time = database.actionTime;

            timeBar.SetActive(true);
            Vector2 timeVeg = timeSpr.transform.localScale;
            timeVeg.x = 0;
            timeSpr.transform.localScale = timeVeg;
            CheckGrowth();
            if (index == 2) {
                for (int i = 0; i < data.harvest_index.Count; i++)
                    tree.transform.GetChild(i).gameObject.SetActive(data.harvest_index.Contains(i));
            }
        } else {
            _available = false;
            plantIcon.SetActive(true);
        }
    }

    public void CheckGrowth() {
        if (data == null || (data != null && data.type == "") || index == 1) return;
        TimeSpan difTime = DateTime.Now - data.stampTime;
        float min = (difTime.TotalMinutes > 30f) ? 30f : (float)difTime.TotalMinutes;
        int ind = Mathf.FloorToInt(min / database.growthTime);
        // print("min : " + min + " ind : " + ind);
        Vector2 timeVeg = timeSpr.transform.localScale;
        timeVeg.x = min;
        timeSpr.transform.localScale = timeVeg;

        if (ind > 1) ind = 1;
        if (ind != index) {

            if (tree) Destroy(tree);
            index = ind;
            UnityEngine.Object obj;
            obj = Resources.Load("Upgrade/Farm/Veg/" + data.type + "_" + index);
            tree = Instantiate(obj, transform) as GameObject;

            if (ind == 1) {
                if (!paricle.activeSelf) {
                    paricle.SetActive(true);
                    timeBar.SetActive(false);
                    timeSpr.transform.localScale = Vector3.one;
                }
            }
        }
    }

    public void Plant(string vegName) {
        if (data.type != "") return;
        data.type = vegName;
        data.stampTime = DateTime.Now;
        database = gameManager.database.vegetables.Select(v => v).Where(p => p.name.Equals(data.type)).FirstOrDefault();
        coin = database.currency.coin;
        exp = database.currency.expReward;
       
        time = database.actionTime;
        data.numVeg = 4;
        data.harvest_index.Clear();
        for (int i = 0; i < 4; i++) data.harvest_index.Add(i);
        index = -1;

        timeBar.SetActive(true);
        Vector2 timeVeg = timeSpr.transform.localScale;
        timeVeg.x = 0;
        timeSpr.transform.localScale = timeVeg;


        CheckGrowth();
        _available = true;
        plantIcon.SetActive(false);
    }

    

    public void Gather(GameObject obj, Vector3 pos)
    {
        if (index == 1)
        {
            SoundManager.Ins.PlaySFX("Pull");
            harvest.CreateProduct(data.type, pos);
            data.numVeg --;
            if (data.numVeg == 0)
            {
                
                 paricle.SetActive(false);
                
                if (tree) Destroy(tree);
                index = -1;
                data.type = "";
                _available = false;
                plantIcon.SetActive(true);
            } else
            {
                int rnd = UnityEngine.Random.Range(0, data.harvest_index.Count);
                int i = data.harvest_index[rnd];
                data.harvest_index.RemoveAt(rnd);
                // tree.transform.GetChild(i).gameObject.SetActive(false);
                 obj.SetActive(false);
            }
        }
    }

    public void ShowSeedBagUI() {
        if (gameManager == null) gameManager = GameManager.Ins;
        gameManager.uiManager.panelUIManager.ShowUIVegMenu(this);
    }
}
