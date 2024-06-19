using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public float growTime = 20;
    public GameObject grassObj;

    [HideInInspector]
    public FarmSceneGame farmScene;
    //[HideInInspector]
    //public Transform targetKeep;

    List<Transform> grassChild;
    //List<Grass> grassList = new List<Grass>();

    SaveManager save;
    GameManager gameManager;

    private void Awake() {
        grassChild = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++) {
            grassChild.Add(transform.GetChild(i));
        }
    }

    private void Start() {
        save = SaveManager.Ins;
        gameManager = GameManager.Ins;
    }

    public void CheckGrassGrow() {
        if (save.data.grass_data.grass_grow.Count == 9) return;
        TimeSpan difTime = DateTime.Now - save.data.grass_data.stampTime;
        if (difTime.TotalMinutes > growTime) {
            save.data.grass_data.stampTime = DateTime.Now;
            GameObject obj;
            Grass grass;
            for (int i = 0; i < grassChild.Count; i++) {
                if (!save.data.grass_data.grass_grow.Contains(i)) {
                    obj = Instantiate(grassObj, grassChild[i]);
                    grass = obj.GetComponent<Grass>();
                    grass.manager = this;
                    grass.index = i;
                    save.data.grass_data.grass_grow.Add(i);
                }
            }
        }
    }


    public void LoadGrass() {
        if (save == null) save = SaveManager.Ins;
        if (gameManager == null) gameManager = GameManager.Ins;
        //targetKeep = gameManager.uiManager.menu.grassMenu.transform;
        GameObject obj;
        Grass grass;
        for (int i = 0; i < grassChild.Count; i++) {
            if (save.data.grass_data.grass_grow.Contains(i)) {
                obj = Instantiate(grassObj, grassChild[i]);
                grass = obj.GetComponent<Grass>();
                grass.manager = this;
                grass.index = i;
                //grassList.Add(grass);
            }
        }
        if (save.data.grass_data.grass_grow.Count == 9) save.data.grass_data.stampTime = DateTime.Now;
        //gameManager.uiManager.menu.SetTextGrass();
    }
   
    public void GetGrass(Grass grass) {
        if (save.data.grass_data.grass_grow.Contains(grass.index)) save.data.grass_data.grass_grow.Remove(grass.index);
        //if (save.data.grass_data.grass_grow.Count == 0) save.data.grass_data.stampTime = DateTime.Now;
        //save.data.grass_data.totalGrass += grass.value;
        //gameManager.uiManager.menu.SetTextGrass();
        grass.livestock.Feed();
    }

    /*public void RemoveGrass(int value) {
        save.data.grass_data.totalGrass -= value;
        //gameManager.uiManager.menu.SetTextGrass();
    }

    public bool CheckGrassEnough(int nGrass) {
        if(save.data.grass_data.totalGrass >= nGrass) {
            RemoveGrass(nGrass);
            return true;
        }else {
            gameManager.uiManager.menu.GrassAlert();
            return false;
        }
    }*/
}
