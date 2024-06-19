using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening.Core.Easing;

public class PanelUIManager : MonoBehaviour
{
   
    public DecorateController decorateController;
    public FoodController foodController;
    public UnlockBoxController unlockBox;
    public VegController vegController;
    public MiniMapController miniMap;
    public ItemsNewController itemsNew;
    public CheckBoxController checkBox;
    public SettingController setting;
    public CollectionBook category;
    public RewardController rewardController;
    public QuestController questController;
    public InventoryController inventory;
    public MarketControl market;
    SoundManager soundManager;

    private void Start() {
        soundManager = SoundManager.Ins;
    }
    public void ShowUICategory() {
        if (GameManager.Ins.isOpenMenu) return;
        GameManager.Ins.isOpenMenu = true;
        category.panel.transform.gameObject.SetActive(true);
        category.GetDatabase();
        
    }

    public void ShowUIInventory()
    {
        if (GameManager.Ins.isOpenMenu) return;
        GameManager.Ins.isOpenMenu = true;
        inventory.panel.gameObject.SetActive(true);
        inventory.CreateInventory();

    }

    public void ShowUIMarket() {
        if (GameManager.Ins.isOpenMenu) return;

        GameManager.Ins.isOpenMenu = true;
        market.panel.gameObject.SetActive(true);
        market.CreateMarket();

    }

    public void ShowUIUpgrade(ObjectUpgrade objUpgrade){
        soundManager.PlaySFX("Show");
        decorateController.gameObject.SetActive(true);
        decorateController.Show(objUpgrade);
    }

    public void ShowUIFoodMenu() {
        soundManager.PlaySFX("Show");
        GameManager.Ins.uiManager.menu.foodNoti.SetActive(false);
        foodController.gameObject.SetActive(true);
        foodController.Show();
    }

    public void ShowUIQuest()
    {
        soundManager.PlaySFX("Show");

        questController.ShowUIQuest();
    }
    

    public void ShowUIMiniMap() {
        SoundManager.Ins.PlaySFX("Motion1");
        miniMap.gameObject.SetActive(true);
    }

    public void ShowUIVegMenu(Plot plot) {
        soundManager.PlaySFX("Show");
        vegController.gameObject.SetActive(true);
        vegController.Show(plot);
 
    }

    public void ShowUIVegMenu() {
        soundManager.PlaySFX("Show");
        vegController.gameObject.SetActive(true);
        vegController.Show();
    }

    public void ShowUnlockLocation(MiniMapController miniMap) {
        soundManager.PlaySFX("Pop");
        unlockBox.gameObject.SetActive(true);
        unlockBox.ShowUnlockMiniMap(miniMap);
    }

    public void ShowUnlockSign(ItemDatabase itemDatabase, AUpgrade aUpgrade ) {
        soundManager.PlaySFX("Pop");
        unlockBox.gameObject.SetActive(true);
        unlockBox.ShowDataUnlock(itemDatabase, aUpgrade);
    }
    public void ShowUnlockPlot(AUpgrade aUpgrade) {
        soundManager.PlaySFX("Pop");
        unlockBox.gameObject.SetActive(true);
        unlockBox.ShowUnlockPlot(aUpgrade);
    }

    public void ShowAlertCheckBox(bool money1,bool money2) {
        soundManager.PlaySFX("Pop");
        checkBox.gameObject.SetActive(true);
        checkBox.ShowBox(money1, money2);
    }

    public void ShowReward(string _id,string function) {
        soundManager.PlaySFX("Pop");
        rewardController.panelParent.SetActive(true);
        rewardController.currentFunction= function;
        rewardController.ShowReward(_id);
    }

    public void ShowUISetting() {
        setting.gameObject.SetActive(true);
    }
}

