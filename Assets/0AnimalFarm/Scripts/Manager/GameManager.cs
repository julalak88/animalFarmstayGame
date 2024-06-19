using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins = null;

    [HideInInspector]
    public Database database;
    [HideInInspector]
    public SceneGameManager sceneManager;
    [HideInInspector]
    public CustomerManager customerManager;
    [HideInInspector]
    public VillageraManager villagerManager;
    [HideInInspector]
    public UIManager uiManager;
    [HideInInspector]
    public UnlockManager unlockManager;
    [HideInInspector]
    public LanguageDatabase loc;
    [HideInInspector]
    public ParticlesManager particles;
    [HideInInspector]
    public Vector3 currentBuilding;
    [HideInInspector]
    public bool isOpenMenu;
    SaveData save;
    private void Awake() {

        Application.targetFrameRate = 60;

        Ins = this;
       
        database = GameObject.Find("Database").GetComponent<Database>();
        GameObject scenes = GameObject.Find("Scenes");
        sceneManager = scenes.GetComponent<SceneGameManager>();
        customerManager = scenes.GetComponent<CustomerManager>();
        villagerManager = scenes.GetComponent<VillageraManager>();
        uiManager = scenes.GetComponent<UIManager>();
        unlockManager = GetComponent<UnlockManager>();
        particles = GetComponent<ParticlesManager>();
        loc = GameObject.Find("Database").GetComponent<LanguageDatabase>();
    }

    private void Start() {
        save = SaveManager.Ins.data;
        int totalCustomer = save.unlock_customer_queue.Count + save.unlocked_customer.Count;
   
        customerManager.InitCustomers(save.unlocked_customer);
        villagerManager.InitCustomers(save.unlocked_villager);
        DOVirtual.DelayedCall(1, StartGame);
    }

    private void StartGame() {
        customerManager.LoadCustomer();
        villagerManager.LoadCustomer();
        sceneManager.LoadCoin();
    }

    public void SaveGame() {
        customerManager.SaveCustomers();
        villagerManager.SaveCustomers();
        sceneManager.SaveCoin();
        SaveManager.Ins.Save();
    }

     void OnApplicationPause(bool pause) {
        print("pppppppppppppppppppp " + pause);
        if (pause) {
             SaveGame();
        }
    }
}
