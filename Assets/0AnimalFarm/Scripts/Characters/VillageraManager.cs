using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class VillageraManager : MonoBehaviour
{
    public List<GameObject> customers;
    public Dictionary<string, GameObject> customerDict = new Dictionary<string, GameObject>();
    SceneGameManager sceneManager;
    SaveManager saveManager;
    List<Villager> characters = new List<Villager>();
    void Start()
    {
        sceneManager = GameManager.Ins.sceneManager;
       
        saveManager = SaveManager.Ins;
       
    }


    public void SaveCustomers()
    {
        if (saveManager == null) saveManager = SaveManager.Ins;
        if (sceneManager == null) sceneManager = GameManager.Ins.sceneManager;

        print("Save Customers");
        saveManager.data.customer_save.Clear();
        VillagerSaveData data;
        Character _char;
        for (int i = 0; i < characters.Count; i++)
        {
            _char = characters[i];
            if (_char.numActivity == 0) continue;
            data = new VillagerSaveData();
            data.customerName = _char.name;
            data.currentScene = _char.currentScene.name;
            data.position = _char.transform.position;
            data.activityRemain = _char.numActivity;
            saveManager.data.villager_save.Add(data);
        }
    }

    public void LoadCustomer()
    {
        if (saveManager == null) saveManager = SaveManager.Ins;
        if (sceneManager == null) sceneManager = GameManager.Ins.sceneManager;
       
        int length = saveManager.data.villager_save.Count;
        VillagerSaveData data;
        Villager _char;
        for (int i = 0; i < length; i++)
        {
            data = saveManager.data.villager_save[i];
            _char = Instantiate(customerDict[data.customerName], transform).GetComponent<Villager>();
            _char.transform.position = data.position;
            _char.name = data.customerName;
            
           // _char.OnStart(false);
            _char.currentScene = sceneManager.scenes[data.currentScene];
            _char.numActivity = data.activityRemain;
            _char.RandomRequest();
            characters.Add(_char);
        }
        saveManager.data.villager_save.Clear();
       
    }

    public void CreateVillager(string villagerName)
    {

    }

    public void InitCustomers(List<string> _customers)
    {
        string cusName;
        for (int i = 0; i < _customers.Count; i++)
        {
            cusName = _customers[i];
            GameObject cusObj = (GameObject)Resources.Load("Customers/" + cusName);
            customers.Add(cusObj);
            customerDict.Add(cusName, cusObj);
        }
    }

}
