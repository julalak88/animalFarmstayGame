using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using I2.Loc;
using TMPro;
public class DecorateSlot : MonoBehaviour
{
    //[HideInInspector]
    public ItemDatabase item;
    [Header("LanguageSelector And Text")]
    public LanguageSelector itemName;
    public LanguageSelector description;
    public LanguageSelector coinText;
    public LanguageSelector priceText;
    public LanguageSelector expText;
    public LanguageSelector requireText;
    public LanguageSelector requireValue;
    [Header("UI")]
    public Image image;
    public Button buyBtn;
    public Button notuseBtn;

    [Header("Gameobject")]
    public GameObject useBtn;
    public GameObject groupRequired;
    public GameObject groupReciveCoin;
    public GameObject groupReciveExp;

    [Header("Animation")]
    public Ease scaleIn = Ease.Flash;
    public Ease scaleOut = Ease.Flash;
    public float time;

    string lang;
    SaveManager saveManager;
    SoundManager soundManager;
    DescriptionData desc_lang;
    [HideInInspector]
    public statusItems statusItem;
    [HideInInspector]
    public DecorateController decorateController;
    void Awake() {
        saveManager = SaveManager.Ins;
        lang = saveManager.lang;
        soundManager = SoundManager.Ins;
      
    
    }

    public void SetItem(ItemDatabase itemDatabase) {
        item = itemDatabase;

        if (item.currency.coin == 0) groupReciveCoin.SetActive(false);
        coinText.Text = "ได้รับเหรียญเพิ่ม +" + item.currency.coin.ToString();

        if (item.currency.expReward == 0) groupReciveExp.SetActive(false);
        expText.Text = "ได้รับดาวเพิ่ม +" + item.currency.expReward.ToString();


        if (item.currency.reqExp <= 0)
        {
            groupRequired.SetActive(false);
        } else
        {
            requireText.Text = item.currency.reqExp.ToString();
            requireValue.Text = "ใช้ดาวปลดล็อค";
        }

        if (!saveManager) saveManager = SaveManager.Ins;
        lang = saveManager.lang;

        desc_lang = item.GetType().GetField(lang).GetValue(item) as DescriptionData;

        itemName.Text = desc_lang.name;
        description.Text = desc_lang.desc;
        priceText.Text = item.currency.price.ToString();
        if (item.thumbnail != null) {
            image.sprite = item.thumbnail;
            image.gameObject.transform.localScale = Vector3.one;
            //if (item.thumbnail.rect.size.x <= 250) {
            //    image.gameObject.transform.localScale = Vector3.one;
            //} else if ((item.thumbnail.rect.size.x <= 350) && (item.thumbnail.rect.size.x > 250)) {
            //    image.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            //} else if (item.thumbnail.rect.size.x > 350) {
            //    image.gameObject.transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);
            //}
        } else {
            image.sprite = item.image;
            if (item.image.rect.size.x <= 250)
            {
                image.gameObject.transform.localScale = Vector3.one;
            } else if ((item.image.rect.size.x <= 350) && (item.image.rect.size.x > 250))
            {
                image.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            } else if (item.image.rect.size.x > 350)
            {
                image.gameObject.transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);
            }
        }
       
        image.SetNativeSize();

        if (CheckExpValue()) {
            buyBtn.interactable = true;
        } else {
            buyBtn.interactable = false;
        }
    }

    public void Active() {
        statusItem = statusItems.use;
        useBtn.gameObject.SetActive(true);
        notuseBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        useBtn.transform.DOScale(1, time).SetEase(scaleIn);

    }

 

    public void ChangeDecorate() {
        statusItem = statusItems.owner;
        useBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        notuseBtn.gameObject.SetActive(true);
        notuseBtn.interactable = true;
     
      // ChooseItem(false);
       

    }

    public void UsedItem() {
        ChooseItem(false);
    }

    void ChooseItem(bool status) {
        soundManager.PlaySFX("Click");
       // if (!status) return;
        decorateController.SaveItem(this, status);

    }

    public void Bought() {
        statusItem = statusItems.buy;
       // useBtn.gameObject.SetActive(false);
       // notuseBtn.gameObject.SetActive(false);

       
            if (CheckMoneyValue()) {
            
                soundManager.PlaySFX("Click");
                
                var cancel = new AlertPopup.ActionButton("ยกเลิก", null, new Color(0.9f, 0.9f, 0.9f));
                var ok = new AlertPopup.ActionButton("ตกลง", () => {
                    buyBtn.transform.DOScale(0, time).SetEase(scaleOut);
                    OnUpgradeItem();
                }, new Color(0f, 0.9f, 0.9f));
                AlertPopup.ActionButton[] buttons = { cancel, ok };
            GameManager.Ins.uiManager.alertPopup.ShowDialog("ต้องการซื้อ", desc_lang.name, buttons);

            } else {

                var ok = new AlertPopup.ActionButton("ตกลง", null, new Color(0.9f, 0.9f, 0.9f));
                AlertPopup.ActionButton[] buttons = { ok };
               GameManager.Ins.uiManager.alertPopup.ShowDialog("", "มีเหรียญไม่พอ", buttons);
            }
          
       
    }


    void OnUpgradeItem() {
        soundManager.PlaySFX("Click");
        decorateController.BuyItem(item.currency.price);
      //  if (decorateController.objectUpgrade.targets.Count == 0) {
           // if (item.currency.expReward > 0) decorateController.AddValue(item.currency.expReward);
       // }

        ChooseItem(true);
    }

    public void ClearBought() {
        soundManager.PlaySFX("Click");
        //groupAlert.SetActive(false);
        buyBtn.gameObject.SetActive(true);
        buyBtn.transform.localScale = Vector3.zero;
        buyBtn.transform.DOScale(1, time).SetEase(scaleIn);
        Bought();

    }

    public bool CheckMoneyValue() {

        return (saveManager.data.coin >= item.currency.price);
       // return (saveManager.data.coin >= item.price && saveManager.data.exp >= item.expRequire);
    }

    public bool CheckExpValue() {

        return (saveManager.data.exp >= item.currency.reqExp);
    }

}
