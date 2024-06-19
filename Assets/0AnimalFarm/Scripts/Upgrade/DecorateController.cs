using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class DecorateController : MonoBehaviour
{
    public Transform contentPos;
    public GameObject slotItemPrefab;
    public LanguageSelector textHead;
    public Scrollbar scrollbar;
    [HideInInspector]
    public ObjectUpgrade objectUpgrade;
    public Image fadeBg;
    public Button closeBtn;
    string currentScene;
    SaveManager save;
    public RectTransform panel;
    public float time;
    public Ease moveIn = Ease.Flash;
    public Ease moveOut = Ease.Flash;
    GameManager gameManager;
    string tempItem;
    string objectTemp;
    DecorateSlot currentSlot;
    DecorateSlot currentUse;
    ItemList itemList;
 
 
    void Start() {
      
        gameManager = GameManager.Ins;
        save = SaveManager.Ins;
        itemList = gameManager.database.itemList;
    }

  

    public void Show(ObjectUpgrade objectUpgrade) {
        GameManager.Ins.uiManager.alertPopup.SetRaycasts(true);
        ClearTemp();
        this.objectUpgrade = objectUpgrade;
        if (gameManager == null) gameManager = GameManager.Ins;
        if (itemList == null) itemList = gameManager.database.itemList;
        currentScene = gameManager.sceneManager.sceneChanger.currentScene.name;

        if (save == null) save = SaveManager.Ins;

        string objectName = objectUpgrade.name;
        if (objectUpgrade.name.Contains("Table")) objectName = "Table";

        if (objectUpgrade is Guesthouse)
            if (objectUpgrade.name.Contains("House")) objectName = "House";

        List<string> upgraded = new List<string>();
     
        if (save.data.upgrade[currentScene].ContainsKey(objectUpgrade.name)) {
            upgraded = save.data.upgrade[currentScene][objectUpgrade.name];
        }

        string itemName;
        List<Transform> objLast = new List<Transform>();

        List<ItemDatabase> list = new List<ItemDatabase>();
        if (objectUpgrade.isGlobal) list = itemList.items["Global"][objectName];
        else list = itemList.items[currentScene][objectName];

        for (int i = 0; i < list.Count; i++) {
           
                DecorateSlot slot = Instantiate(slotItemPrefab, contentPos, false).GetComponent<DecorateSlot>();
                slot.decorateController = this;
                slot.SetItem(list[i]);
                itemName = slot.item.name;
                if (upgraded.Contains(itemName)) {
                    if (upgraded.IndexOf(itemName) == 0) {
                        slot.Active();
                        currentSlot = slot;
                        currentUse = slot;
                        objectTemp = itemName;
                    } else {
                        slot.ChangeDecorate();
                    }


                    objLast.Add(slot.transform);
                    slot.groupRequired.SetActive(false);

                } else {
                   // slot.groupRequired.SetActive(true);

                }
 
        }

        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, time);
        panel.DOAnchorPosY(0, time).SetEase(moveIn);
        objLast.Select(c => c).ToList().ForEach(cc => cc.transform.SetAsLastSibling());
        objLast.Clear();

        SetHeader(objectUpgrade.headerKey);
    }

    void SetHeader(string nameHead) {
        if (nameHead.Equals("House")) {
            textHead.Text = "บ้านของเรา";
        } else if (nameHead.Equals("Flowers")) {
            textHead.Text = "แปลงดอกไม้";
        } else if (nameHead.Equals("Fence")) {
            textHead.Text = "รั้วแสนสวย";
        } else if (nameHead.Equals("Shop")) {
            textHead.Text = "ร้นค้า";
        } else if (nameHead.Equals("Lamp")) {
            textHead.Text = "โคมไฟ";
        } else if (nameHead.Equals("Decoration")) {
            textHead.Text = "ศาลาคนเศร้า";
        } else if (nameHead.Equals("Mailbox")) {
            textHead.Text = "ตู้ไปรษณีย์ที่รัก";
        } else if (nameHead.Equals("Barn")){
            textHead.Text = "มุมแอบงีบ";
        } else if (nameHead.Equals("Pond")) {
            textHead.Text = "บ่อน้ำจืด";
        } else if (nameHead.Equals("Cart")){
            textHead.Text = "รถเข็นผักผลไม้";
        } else if (nameHead.Contains("Table"))
        {
            textHead.Text = "โต๊ะแสนสวย";
        } else if (nameHead.Contains("Foodtruck"))
        {
            textHead.Text = "รถขายอาหาร";
        } else if (nameHead.Contains("Kitchen"))
        {
            textHead.Text = "เตาทำอาหาร";
        }
    }

    void ClearTemp() {

        tempItem = string.Empty;
        currentSlot = null;
        currentUse = null;
    }


    public void SaveItem(DecorateSlot itemSlot, bool newItem) {

        save.SaveItem(itemSlot.item, currentScene, objectUpgrade.name);

        tempItem = itemSlot.item.name;
       
        if (newItem) {


            fadeBg.DOFade(0, 0.01f);
            Vector2 pos = panel.transform.localPosition;
            panel.transform.localPosition = new Vector2(pos.x, 1950);
            gameManager.uiManager.menu.HideUpgradeIcon();
            foreach (Transform child in contentPos) {
                Destroy(child.gameObject);
            }
            scrollbar.value = 1;
            if (!string.IsNullOrEmpty(tempItem)) objectUpgrade.LoadObject(currentScene, tempItem, true, true);
            AddValue(itemSlot.item);
            SoundManager.Ins.PlaySFX("NewItem");
            gameObject.SetActive(false);

        } else {
            if (currentSlot != null) {
                itemSlot.Active();
                if (currentSlot.statusItem == statusItems.owner) {
                    currentSlot.ChangeDecorate();
                } else if (currentSlot.statusItem == statusItems.buy) {
                    currentSlot.ClearBought();
                }

            }
            if (currentUse != null) {
                currentUse.ChangeDecorate();
            }
            currentSlot = itemSlot;
            currentUse = itemSlot;
        }
    }


    public void BuyItem(int value) {
      
        gameManager.uiManager.money.RemoveCoin(value);
    }

    public void AddValue(ItemDatabase item) {//add candy first buy
       
       // gameManager.unlockManager.CheckForUnlock(item);
       
        if (item.currency.expReward > 0)
        {
            Coin coin = gameManager.sceneManager.CreateCoin(gameManager.currentBuilding, gameManager.uiManager.money.collectPos.position);
            coin.isCoin = false;
            coin.value = Convert.ToInt32(item.currency.expReward);
        }


    }
    
    public void CheckAllSlots(DecorateSlot itemSlot) {
        ClearTemp();
        if (currentSlot != null) {
 
            currentSlot.buyBtn.gameObject.SetActive(true);
        }
        currentSlot = itemSlot;
    }

 
    

    public void ShowAlertBox(bool money1, bool money2) {
        gameManager.uiManager.panelUIManager.ShowAlertCheckBox(money1, money2);
    }

    public void ClosePanel() {
      
        fadeBg.DOFade(0, time);
        gameManager.uiManager.alertPopup.SetRaycasts(false);
        panel.DOAnchorPosY(1950, time).SetEase(moveOut).OnComplete(() => {
            foreach (Transform child in contentPos) {
                Destroy(child.gameObject);
            }
            scrollbar.value = 1;
           
            if (!string.IsNullOrEmpty(tempItem) && !objectTemp.Contains(tempItem)) {
                objectUpgrade.LoadObject(currentScene, tempItem, true);
            }
            ClearTemp();
            gameObject.SetActive(false);
        });
    }

}

[Serializable]
public enum statusItems { use, owner, buy }
