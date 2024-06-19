using System;
using System.Collections;
using System.Collections.Generic;
using unityad;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public GameObject adNoti;
    public GameObject foodNoti;
    public GameObject cancelUpgrade;
    public GameObject allMenu;
    public GameObject foodMenu;
    public Button settingBtn;
    ADManager adManager; 
    CustomerManager customerManager;
    UIManager uiManager;
    SaveManager save;

    private void Start() {
        adNoti.SetActive(false);
        foodNoti.SetActive(false);
        cancelUpgrade.SetActive(false);
        ADManager.OnADReady += OnADReady;

        adManager = ADManager.Ins;
        customerManager = GameManager.Ins.customerManager;
        uiManager = GameManager.Ins.uiManager;
        save = SaveManager.Ins;
        AddOnClickSetting();


    }

    public void OnNotificationReady(bool value) {
        adNoti.transform.parent.gameObject.SetActive(value);
    }

    #region promote and AD
    private void OnADReady() {
        adNoti.SetActive(true);
    }

    public void OnWatchAD() {
        //if (adNoti.transform.parent.gameObject.activeInHierarchy && adNoti.activeInHierarchy)
           // adManager.ShowRewardAD(onWatchedAD, null);
    }

    private void onWatchedAD() {
        adNoti.SetActive(false);
        customerManager.AddMoreCustomers();
    }
    #endregion

    public void ShowUpgradeIcon() {
        GameManager.Ins.isOpenMenu = true;
        SoundManager.Ins.PlaySFX("Motion1");
        uiManager.alertPopup.SetRaycasts(false);
        uiManager.ShowUpgradeIconInCurrentScene();
    }

    public void HideUpgradeIcon() {
        SoundManager.Ins.PlaySFX("Click2");
        GameManager.Ins.isOpenMenu = false;
        uiManager.alertPopup.SetRaycasts(false);
        uiManager.HideUpgradeIconInCurrentScene();
    }

    public void AddOnClickSetting() {
        settingBtn.onClick.AddListener(uiManager.panelUIManager.ShowUISetting);
    }

    public void ShowInventory() {

    }
  


}
