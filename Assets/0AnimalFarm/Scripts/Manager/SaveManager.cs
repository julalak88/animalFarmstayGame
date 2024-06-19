
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;
 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Xml.Linq;

public class SaveManager : SerializedMonoBehaviour {

    public static SaveManager Ins = null;
    public SaveData data;

    [HideInInspector]
    public UnityEvent onLoginSuccess = new UnityEvent(), onUploadSuccess = new UnityEvent();
    [HideInInspector]
    public DownloadSuccess onDownloadSuccess = new DownloadSuccess();

    [HideInInspector]
    public string login = "";
    [HideInInspector]
    public DateTime uploadTime;
    [HideInInspector]
    public string lang = "TH";

    public bool test;

    void Awake() {
        Ins = this;

      
        Load();
    }

    public void NewGame()
    {
        ES3.DeleteFile();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Game");
    }

    public void Save() {
        ES3.Save("data", data);
    }

    public void Load() {

        if (data != null) data = null;
        if (ES3.KeyExists("data")) {
             data = ES3.Load<SaveData>("data");
           // LoadData();
        }

        if (test) {
            lang = "TH";
            SetNewData();
        }
    }

    void LoadData()
    {

        if (data.unlock_quest != null) GameManager.Ins.uiManager.panelUIManager.questController.SetQuest(data.unlock_quest.questId, data.unlock_quest);
    }

    public void SetNewData() {
        //print("data " + data);
        if (data == null) {
            data = new SaveData();
            data.coin = 5000000;
            data.exp = 12000;
            data.expVeg = 13;
        }

        if (data.unlocked_customer.Count == 0)
        {
            data.unlocked_customer.Add("gst0000");
            data.unlocked_customer.Add("gst0001");
            data.unlocked_customer.Add("gst0002");
            data.unlocked_customer.Add("gst0003");
          
        }

        if (data.unlocked_villager.Count == 0)
        {
            data.unlocked_villager.Add("vil0000");
            data.unlocked_villager.Add("vil0001");
            

        }

        if (data.unlocked_scene.Count == 0) {
            data.unlocked_scene.Add("Home"); 
           
        }

        if (!data.upgrade.ContainsKey("Home")) {
            data.upgrade["Home"] = new Dictionary<string, List<string>>();
            data.upgrade["Home"]["House"] = new List<string>() { "hou0000" };
            data.upgrade["Home"]["Flowers"] = new List<string>() { "flw0000" };
            data.upgrade["Home"]["Fence"] = new List<string>() { "fen0000" };
            data.upgrade["Home"]["Shop"] = new List<string>() { "shp0000" };
            data.upgrade["Home"]["Mailbox"] = new List<string>() { "mal0000" };
            data.upgrade["Home"]["Decoration"] = new List<string>() { "dec0000" };
            data.upgrade["Home"]["Lamp"] = new List<string>() { "lam0000" };
            data.upgrade["Home"]["Bench"] = new List<string>() { "ben0000" };
        }


        if (data.unlocked_food.Count == 0)
        {
            data.unlocked_food.Add("fod0007"); data.unlocked_food.Add("fod0004");
            
        }

        if (data.inventory.Count == 0) {

            data.inventory = new Dictionary<string, Inventory>();
            Inventory data1 = new Inventory();
            data1.total = 80;
            data1.idname = "veg0000";
            data1.plantType = PlantType.vegetable;

            Inventory data2 = new Inventory();
            data2.total = 5;
            data2.idname = "veg0001";
            data2.plantType = PlantType.vegetable;

            Inventory data3 = new Inventory();
            data3.total = 5;
            data3.idname = "veg0002";
            data3.plantType = PlantType.vegetable;

            Inventory data4 = new Inventory();
            data4.total = 5;
            data4.idname = "veg0003";
            data4.plantType = PlantType.vegetable;

            Inventory data5 = new Inventory();
            data5.total = 50;
            data5.idname = "veg0005";
            data5.plantType = PlantType.vegetable;

            data.inventory.Add(data1.idname, data1);
            data.inventory.Add(data4.idname, data4);
            data.inventory.Add(data2.idname, data2);
            data.inventory.Add(data3.idname, data3);
            data.inventory.Add(data5.idname, data5);
            
        }


        //if (data.unlocked_scene.Count == 0) {
        //    data.unlocked_scene.Add("Home"); data.unlocked_scene.Add("Riverside"); data.unlocked_scene.Add("Farm"); data.unlocked_scene.Add("Resort");
        //}
        //if (!data.upgrade.ContainsKey("Home")) {
        //    data.upgrade["Home"] = new Dictionary<string, List<string>>();
        //    data.upgrade["Home"]["House"] = new List<string>() { "house0"};
        //    data.upgrade["Home"]["Flowers"] = new List<string>() { "flowers1"};
        //    data.upgrade["Home"]["Fence"] = new List<string>() { "fence0" };
        //    data.upgrade["Home"]["Shop"] = new List<string>() { "shop0" };
        //    data.upgrade["Home"]["Mailbox"] = new List<string>() { "mailbox1"};
        //    data.upgrade["Home"]["Decoration"] = new List<string>() { "decoration0" };
        //    data.upgrade["Home"]["Lamp"] = new List<string>() { "lamp0" };
        //}


        //if (!data.upgrade.ContainsKey("Resort")) {
        //    data.upgrade["Resort"] = new Dictionary<string, List<string>>();
        //    data.upgrade["Resort"]["HouseA"] = new List<string>() { "house0" };
        //    data.upgrade["Resort"]["HouseB"] = new List<string>() { "house0" };
        //    data.upgrade["Resort"]["Bridge"] = new List<string>() { "bridge0" };
        //    data.upgrade["Resort"]["Shop"] = new List<string>() { "shop0" };
        //}
        //if (!data.upgrade.ContainsKey("Riverside")) {
        //    data.upgrade["Riverside"] = new Dictionary<string, List<string>>();
        //    data.upgrade["Riverside"]["FoodtruckA"] = new List<string>() { "foodtrucka1" };
        //    data.upgrade["Riverside"]["TableA"] = new List<string>() { "table2" };
        //    data.upgrade["Riverside"]["Kitchen"] = new List<string>() { "kitchen1" };
        //}
        Invoke("Test", 0.5f);
    }

 void Test()
    {
        GameManager.Ins.unlockManager.UnlockScene("Farm");
        GameManager.Ins.unlockManager.UnlockScene("Riverside");

    }

    public void SaveItem(ItemDatabase itemDatabase, string currentScene, string objName) {
        Dictionary<string, List<string>> objects = new Dictionary<string, List<string>>();

        if (!data.upgrade[currentScene].ContainsKey(objName)) {
            objects = data.upgrade[currentScene];
            objects.Add(objName, new List<string> { itemDatabase.name });
        } else {

            if (!data.upgrade[currentScene][objName].Contains(itemDatabase.name)) {
                data.upgrade[currentScene][objName].Add(itemDatabase.name);
            }
        }
        data.upgrade[currentScene][objName].Remove(itemDatabase.name);
        data.upgrade[currentScene][objName].Insert(0, itemDatabase.name);
        
    }


    
    public void SaveFood(string food) {
        data.bought_food.Add(food);
    }

    public void SavePlot(string plot) {
        if (!data.plots.ContainsKey("plot")) data.plots[plot] = new PlotData();
    }

    public void SaveScene(string scene) {
        if (!data.unlocked_scene.Contains(scene)) data.unlocked_scene.Add(scene);
    }

    public void SaveQuest(QuestData _current)
    {
        data.unlock_quest.questId = _current.questId;
        data.unlock_quest.progress = _current.progress;
    }

    public void SaveLanguage() {
      //  SaveGame.Save<string>("lang", lang);
    }

    public void SaveInventory(string _idName,Inventory _inventory,bool havest = true)
    {
       
        if (!data.inventory.ContainsKey(_idName))
        { 
            data.inventory.Add(_idName, _inventory);
           
        } else
        {
            if (_inventory.total <= 0) {
                data.inventory.Remove(_idName);
            } else {
                data.inventory[_idName].idname = _inventory.idname;
                if (havest) {
                    data.inventory[_idName].total += _inventory.total;
                } else {
                    data.inventory[_idName].total = _inventory.total;
                }
                data.inventory[_idName].plantType = _inventory.plantType;
            }

          
        }

    }

    public void RemoveInventory(string _idName, int total, bool havest = true)
    {

        data.inventory[_idName].total = (data.inventory[_idName].total- total);

    }


    #region // --------------------------  cloud save  ----------------------------- //

    public void UploadSave() {
       
    }

    public void DownloadSave() {
       
    }

    public void SaveLogin(string type) {
        login = type;
      //  SaveGame.Save<string>("login", type);
    }

    public void SaveUploadTime() {
      //  SaveGame.Save<DateTime>("uploadTime", DateTime.Now);
    }
    
    /*public void UploadSave() {
        if (FBManager.Ins.user_id == "" || !SaveGame.Exists("save.dat")) return;

        StartCoroutine(DoUpload());
    }

    public void DownloadSave() {
        if (FBManager.Ins.user_id == "") return;

        StartCoroutine(DoDownload());
    }

// The uploading process
    IEnumerator DoUpload() {
        SaveGameWeb web = new SaveGameWeb("http://www.thegameisart.com/smallfarm-paradise-php-savegamepro-mysql/index.php", "d273fe5074bd2f33e17e60762dd3d07d", FBManager.Ins.user_id, FBManager.Ins.user_id);
        yield return web.UploadFile("save.dat", "save.dat");
        if (web.Request.isHttpError || web.Request.isNetworkError) {
            Debug.LogError("Upload Failed");
            Debug.LogError(web.Request.error);
            Debug.LogError(web.Request.downloadHandler.text);
            //log.text = "Upload Failed : " + web.Request.error;
        } else {
            Debug.Log("Upload Successful");
            Debug.Log("Response: " + web.Request.downloadHandler.text);
            //log.text = "Upload Successful : " + web.Request.downloadHandler.text;
        }
    }

    // The downloading process
    IEnumerator DoDownload() {
        SaveGameWeb web = new SaveGameWeb("http://www.thegameisart.com/smallfarm-paradise-php-savegamepro-mysql/index.php", "d273fe5074bd2f33e17e60762dd3d07d", FBManager.Ins.user_id, FBManager.Ins.user_id);
        yield return web.DownloadFile("save.dat", "save.dat");
        if (web.Request.isHttpError || web.Request.isNetworkError) {
            Debug.LogError("Download Failed");
            Debug.LogError(web.Request.error);
            Debug.LogError(web.Request.downloadHandler.text);
            //log.text = "Download Failed : " + web.Request.error;
        } else {
            Debug.Log("Download Successful");
            Debug.Log("Response: " + web.Request.downloadHandler.text);
            //log.text = "Download Successful : " + web.Request.downloadHandler.text;
            Load();
        }
    }*/
}
#endregion

[ShowOdinSerializedPropertiesInInspector]
public class SaveData {

    public DateTime stampTime;
    public int coin = 0;
    public float exp = 0;
    public float expVeg = 0;
    public List<string> unlocked_customer = new List<string>();
    public List<string> unlocked_villager = new List<string>();
    public List<string> unlock_customer_queue = new List<string>();
    public List<string> unlock_postcard = new List<string>();
    public List<string> bought_food = new List<string>();
    public List<string> unlocked_food = new List<string>();
    public QuestData unlock_quest = new QuestData();
    public List<string> unlocked_scene = new List<string>();
    public Dictionary<string, int> record = new Dictionary<string, int>();
    public List<CustomerSaveData> customer_save = new List<CustomerSaveData>();
    public List<VillagerSaveData> villager_save = new List<VillagerSaveData>();
    public List<CoinSaveData> coin_save = new List<CoinSaveData>();
    public Dictionary<string, Dictionary<string,List<string>>> upgrade = new Dictionary<string, Dictionary<string, List<string>>>();
    public Dictionary<string, PlotData> plots = new Dictionary<string, PlotData>();
    public Dictionary<string, Inventory> inventory = new Dictionary<string, Inventory>();
    public Dictionary<string, LivestockData> livestocks = new Dictionary<string, LivestockData>();
    public GrassData grass_data = new GrassData();
}


[System.Serializable]
public class CustomerSaveData {
    public string customerName = "";
    public string currentScene = "";
    public Vector2 position;
    public int activityRemain;
}

[System.Serializable]
public class VillagerSaveData
{
    public string customerName = "";
    public string currentScene = "";
    public Vector2 position;
    public int activityRemain;
}



[System.Serializable]
public class CoinSaveData
{
    public int value;
    public bool isCoin = true;
    public Vector2 position;
}

[System.Serializable]
public class PlotData
{
    public DateTime stampTime;
    public string type = "";
    public int numVeg = 0;
    public bool watering = false;
    public List<int> harvest_index = new List<int>();
}

[System.Serializable]
public class LivestockData
{
    public DateTime stampTime;
    public int numProduct = 0;
    public int food = 0;
}

 
[System.Serializable]
public class GrassData
{
    //public int totalGrass = 0;
    public List<int> grass_grow = new List<int>();
    public DateTime stampTime;
}

[System.Serializable]
public class QuestData
{
    public string questId;
    public int progress;
    public bool complete;
}
[System.Serializable]
public class Inventory
{
    public string idname;
    public int total;
    public PlantType plantType; 
}

[System.Serializable]
public class DownloadSuccess : UnityEvent<bool>
{
}
