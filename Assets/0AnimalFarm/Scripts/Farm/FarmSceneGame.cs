using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSceneGame : SceneGame
{
    public Staff staff;


    GameManager gameManager;
    Plot[] plots;
    Livestock[] livestocks;

    int indexLivestock = -1;

    protected override void Awake() {
        base.Awake();
       
        plots = upgradeTransform.GetComponentsInChildren<Plot>();
        livestocks = upgradeTransform.GetComponentsInChildren<Livestock>();
    }

    protected override void Start() {
        base.Start();
        gameManager = GameManager.Ins;
        UpdatePlotData();
       
    }

    public void UpdatePlotData() {
        Dictionary<string, PlotData> objects = save.data.plots;
        for (int i = 0; i < plots.Length; i++) {
   
            if (objects.ContainsKey(plots[i].name)) {
                plots[i].LoadObject(objects[plots[i].name]);
            } //else {
               // plots[i].Locked();
            //}
        }
        Dictionary<string, LivestockData> objects2 = save.data.livestocks;
        for (int i = 0; i < livestocks.Length; i++) {
            if (!objects2.ContainsKey(livestocks[i].name)) objects2[livestocks[i].name] = new LivestockData();
            livestocks[i].data = objects2[livestocks[i].name];
            //livestocks[i].grassManager = grassManager;
        }

    }

    float bombTimer;
    private void FixedUpdate()
    {
        bombTimer += Time.deltaTime;

        if (bombTimer > 2)
        {
            bombTimer = 0;
            for (int i = 0; i < plots.Length; i++) plots[i].CheckGrowth();
        }
    }


    public override void OnSceneActive() {
        base.OnSceneActive();
      //  for (int i = 0; i < plots.Length; i++) plots[i].CheckGrowth();
     //   for (int i = 0; i < livestocks.Length; i++) livestocks[i].CheckHungry();
       

        FindLeastFood();
    }

    public override void OnSceneInactive() {
        base.OnSceneInactive();
    
    }

    public override void UpdateScene() {
        base.UpdateScene();
        staff.OnUpdate();
    }

    public Livestock GetFeedLivestock() {
      
        if (indexLivestock == -1) return null;
        if (livestocks[indexLivestock].isFoodFull()) {
            FindLeastFood();
            return GetFeedLivestock();
        }
        return livestocks[indexLivestock];
    }

    void FindLeastFood() {
        indexLivestock = -1;
        int f = -1;
        int num;
        for (int i = 0; i < livestocks.Length; i++) {
            if (livestocks[i].isAvailable && !livestocks[i].isFoodFull()) {
                num = livestocks[i].data.food;
                if (f < num) {
                    f = num;
                    indexLivestock = i;
                }
            }
        }
    }
}
