using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore.Text;
using DG.Tweening.Core.Easing;
using static UnityEngine.GraphicsBuffer;

public class UnlockManager : MonoBehaviour
{

    Database database;
    SaveData saveData;
    SaveManager saveManager;
    GameManager gm;
    List<string> unlocked_food;
    List<string> unlocked_cus;
    SceneChanger sceneChanger;
    DialogManager dialog;
    FarmSceneGame farmScene;
    void Start()
    {
        saveData =  SaveManager.Ins.data;
        saveManager =  SaveManager.Ins;
        gm = GameManager.Ins;
        database = gm.database;
        customerManager = gm.customerManager;
        sceneChanger = gm.sceneManager.sceneChanger;
        dialog = gm.uiManager.dialog;
       

    }

    #region // --------------------- customers --------------------- //

    CustomerManager customerManager;
    string newCustomer;

    public void AddRecord(string _name) {
        if (saveData.record.ContainsKey(_name)) {
            if(saveData.record[_name] < 99) saveData.record[_name]++;
        } else saveData.record.Add(_name, 1);
    }
    public void AddRecordOnlyOne(string _name) {
        if (!saveData.record.ContainsKey(_name)) saveData.record.Add(_name, 1);
    }

    public void CheckForNewCustomer() {
        int length = (saveData.unlock_customer_queue.Count < 3) ? saveData.unlock_customer_queue.Count : 3;
        int count;
        string customerName = "", itemName;
        List<UnlockCondition> condition;
        bool correct = false;
        for (int i = 0; i < length; i++) {
            correct = false;
            customerName = saveData.unlock_customer_queue[i];
            if(database.customerDescription.ContainsKey(customerName)) {
              condition = database.customerDescription[customerName].unlock_condition;
                correct = (condition.Count > 0);
                for (int j = 0; j < condition.Count; j++) {
                    itemName = condition[j].itemName;
                    count = condition[j].count;
                    if (saveData.record.ContainsKey(itemName) && saveData.record[itemName] >= count) {
                        saveData.record[itemName] = 1;
                    } else {
                        correct = false;
                        break;
                    }
                }
                if (correct) break;
            }
        }
        if (correct) {
            newCustomer = customerName;
            saveData.unlock_customer_queue.Remove(customerName);
            saveData.unlocked_customer.Add(customerName);
            sceneChanger.enable = false;
            dialog.OnDialogComplete = OnDialogNewCustomerComplete;
            CustomerDatabase data = database.customerDescription[customerName];
            StoryData story = data.GetType().GetField("Story" + SaveManager.Ins.lang).GetValue(data) as StoryData;
            dialog.AddText(story.story);
            dialog.ShowDialog(data.image);
        }
    }

    void OnDialogNewCustomerComplete() {
       // customerManager.AddCustomer(newCustomer);
       // customerManager.CreateCharacter(newCustomer);
    }

    Character currentCharacter= null;
     string currentCharacterName = string.Empty;
    public void TalkToGuest(string _customerName,Character _character)
    {
            currentCharacter = _character;
            currentCharacterName = _customerName;
            sceneChanger.enable = false;
            customerManager.touchToTalk = false;
            dialog.OnDialogComplete = OnRandomQuest;
            CustomerDatabase data = database.customerDescription[_customerName];
            DialogDetail story = data.GetType().GetField("dialog" + SaveManager.Ins.lang).GetValue(data) as DialogDetail;
            dialog.AddText(story.firstmeet);
            dialog.ShowDialog(data.thumbnail);
        
    }
   
    void OnRandomQuest() {
        sceneChanger.enable = true;
  
        customerManager.touchToTalk = true;
        if (currentCharacter.isTimeOut == false)
        {
            currentCharacter.RandomRequest();
        }
        customerManager.AddCustomer(currentCharacterName);
    }
    #endregion // ------------------------------------------------ //

    public void CheckForUnlock(ItemDatabase itemDatabase) {
        List<string> unlockName = new List<string>(); 
        if (itemDatabase.itemsType.Equals(ItemsType.FoodtruckLeft) || itemDatabase.itemsType.Equals(ItemsType.FoodtruckRight) 
            || itemDatabase.itemsType.Equals(ItemsType.Kitchen)) {
            for (int i = 0; i < itemDatabase.unlock_name.Count; i++) {
                if (itemDatabase.unlock_name[i].Contains("fod"))
                {
                    gm.uiManager.menu.foodNoti.SetActive(true);
                    saveData.unlocked_food.Add(itemDatabase.unlock_name[i]);
                    unlockName.Add(itemDatabase.unlock_name[i]);
                } else
                {
                    customerManager.CreateCharacter(itemDatabase.unlock_name[i]);
                }
            }
            if (unlockName.Count > 0) {
                //CheckForUnlock
               // gm.uiManager.panelUIManager.itemsNew.gameObject.SetActive(true);
               // gm.uiManager.panelUIManager.itemsNew.ShowFoodUnlock(unlockName);
            }
        } else
        {
            for (int i = 0; i < itemDatabase.unlock_name.Count; i++)
            {
                
                customerManager.CreateCharacter(itemDatabase.unlock_name[i]);
            }
        }
    }

     
   public bool CheckFoodCanLearn(List<Ingredients> ingredients)
    {
        List<Inventory> invent = new List<Inventory>();
        bool cnaLearn = false;

  
        foreach (Ingredients I in ingredients)
        {
            invent = saveManager.data.inventory.Select(v => v.Value).Where(x => x.idname.Equals(I.itemName)).ToList();
            
         }

        
        for (int i = 0; i < invent.Count; i++)
        {
            if (invent[i].total >= ingredients[i].count)
            {
                cnaLearn = true;
            }
             
        }

        return cnaLearn;
    }

    public bool CheckUnlockFood(string foodName) {
        unlocked_food = saveData.unlocked_food;
        return unlocked_food.Contains(foodName);
    }

    public bool CheckUnlockCustomer(string nameCus) {
        bool haveUnlock = false; ;
        if(saveData.unlocked_customer.Count > 0)
        {
            unlocked_cus = saveData.unlocked_customer;
            if (unlocked_cus.Contains(nameCus))
            {
                haveUnlock = true;
            }
        } 
        return haveUnlock;
    }
    
     


    public void UnlockPostcard(string nameCrad)
    {
        saveData.unlock_postcard.Add(nameCrad);
    }

    public bool CheckUnlockPostcard(string nameCrad)
    {
        return saveData.unlock_postcard.Contains(nameCrad);
    }

    public void UnlockScene(string _name)
    {
        if (saveData.unlocked_scene.Contains(_name)) return;
        saveData.unlocked_scene.Add(_name);

        SetDecorateDefalut(_name);
    }

    void SetDecorateDefalut(string _scene)
    {
        List<string> _name = new List<string>();
        if (_scene.Contains("Farm"))
        {
            if (!saveData.upgrade.ContainsKey("Farm"))
            {
                saveData.upgrade["Farm"] = new Dictionary<string, List<string>>();
                saveData.upgrade["Farm"]["Barn"] = new List<string>() { "ban0000" };
                saveData.upgrade["Farm"]["Pond"] = new List<string>() { "pon0000" };
                saveData.upgrade["Farm"]["Cart"] = new List<string>() { "car0000" };
                _name.Add("ban0000");
                _name.Add("pon0000");
                _name.Add("car0000");
                saveManager.SavePlot("PlotA");
                saveManager.SavePlot("PlotB");
                saveManager.SavePlot("PlotC");
                saveManager.SavePlot("PlotD");
                saveManager.SavePlot("PlotE");
                saveManager.SavePlot("PlotF");
                saveManager.SavePlot("PlotG");
                saveManager.SavePlot("PlotH");
                saveManager.SavePlot("PlotI");
                saveManager.SavePlot("PlotJ");
                saveManager.SavePlot("PlotK");
                saveManager.SavePlot("PlotL");
             
                gm.sceneManager.farmScene.UpdatePlotData();
                gm.sceneManager.farmScene.UpdateObjUpgrades(_name);
               
            }

        } else if (_scene.Contains("Riverside"))
        {
          
           
            if (!saveData.upgrade.ContainsKey("Riverside"))
            {
                
                saveData.upgrade["Riverside"] = new Dictionary<string, List<string>>();
                saveData.upgrade["Riverside"]["FoodtruckLeft"] = new List<string>() { "ftk0000" };
                saveData.upgrade["Riverside"]["FoodtruckRight"] = new List<string>() { "ftk0004" };
                saveData.upgrade["Riverside"]["TableA"] = new List<string>() { "tab0000" };
                saveData.upgrade["Riverside"]["TableB"] = new List<string>() { "tab0000" };
                saveData.upgrade["Riverside"]["TableC"] = new List<string>() { "tab0000" };
                saveData.upgrade["Riverside"]["TableD"] = new List<string>() { "tab0000" };
                saveData.upgrade["Riverside"]["TableE"] = new List<string>() { "tab0000" };
                saveData.upgrade["Riverside"]["Kitchen"] = new List<string>() { "kic0000" };
                _name.Add("ftk0000");
                _name.Add("tab0000");
                _name.Add("tab0000");
                _name.Add("tab0000");
                _name.Add("tab0000");
                _name.Add("tab0000");
                _name.Add("ftk0004");
                _name.Add("kic0000");
                gm.sceneManager.riverside.UpdateObjUpgrades(_name);
                gm.uiManager.menu.foodNoti.SetActive(true);
                //  sceneChanger.MoveToScene(0);
            }
        } else if (_scene.Contains("Resort"))
        {
          //  saveData.upgrade["Resort"] = new Dictionary<string, List<string>>();
          //  saveData.upgrade["Resort"]["HouseA"] = new List<string>() { "res0000" };
          //  saveData.upgrade["Resort"]["HouseB"] = new List<string>() { "res0000" };
          //  saveData.upgrade["Resort"]["HouseC"] = new List<string>() { "res0000" };
          //  saveData.upgrade["Resort"]["HouseD"] = new List<string>() { "res0000" };
          //  saveData.upgrade["Resort"]["HouseE"] = new List<string>() { "res0000" };
          ////  saveData.upgrade["Resort"]["Shop"] = new List<string>() { "sht0000" };
          //  saveData.upgrade["Resort"]["Bridge"] = new List<string>() { "bri0000" };
          ////  saveData.upgrade["Resort"]["Lamp"] = new List<string>() { "lam0000" };
          ////  saveData.upgrade["Resort"]["Square"] = new List<string>() { "squ0000" };
          //  _name.Add("res0000");
          //  _name.Add("res0000");
          //  _name.Add("res0000");
          //  _name.Add("res0000");
          //  _name.Add("res0000");
          // // _name.Add("sht0000");
          // _name.Add("bri0000");
          // // _name.Add("lam0000");
          //  //_name.Add("squ0000");
          //  gm.sceneManager.resort.UpdateObjUpgrades(_name);
          // // sceneChanger.MoveToScene(1);
        }
    }

 
}
