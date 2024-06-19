using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using I2.Loc;
using TMPro;
public class VegSlot : MonoBehaviour
{
    public LanguageSelector seedName;
    public Image pic;
    public Image picLock;
    [HideInInspector]
    public VegController vegController;
    [HideInInspector]
    public VegDatabase vegDatabase;
    public GameObject reqGroup;
    public GameObject buyGroup;
    public GameObject groupReciveExp;
    public GameObject groupReciveCoin;
    public GameObject groupReciveVeg;
    [Header("Textmeshpro")]
    public LanguageSelector coinText;
    public LanguageSelector vegexpText;
    public LanguageSelector expText;
    public LanguageSelector requireText;
    public LanguageSelector requireValue;
    public LanguageSelector priceText;
    DescriptionData desc_lang;

    public float time;
    public Ease scaleIn = Ease.Flash;
    SaveManager saveManager;
    SoundManager soundManager;
    string lang;
    private void Awake() {
        saveManager =  SaveManager.Ins;
        soundManager = SoundManager.Ins;
    }
   
   
    public void Show() {

        if (!saveManager) saveManager = SaveManager.Ins;
        lang = saveManager.lang;

         desc_lang = vegDatabase.GetType().GetField(lang).GetValue(vegDatabase) as DescriptionData;

        seedName.Text = desc_lang.name;
        if (vegDatabase.currency.coin == 0) groupReciveCoin.SetActive(false);
     //   if (vegDatabase.currency.expReward == 0) groupReciveExp.SetActive(false);
        if (vegDatabase.vegExpDrop == 0) groupReciveVeg.SetActive(false);

        if (CheckExpValue())
        {
          
            priceText.Text = vegDatabase.currency.price.ToString();
            reqGroup.SetActive(false);
            pic.gameObject.SetActive(true);
            if (pic.sprite == null) pic.sprite = vegDatabase.image;
        } else
        {
            buyGroup.SetActive(false);
            if (vegDatabase.currency.reqExp == 0) reqGroup.SetActive(false);
            picLock.gameObject.SetActive(true);
            if (picLock.sprite == null) picLock.sprite = vegDatabase.image;
             
            requireText.Text = vegDatabase.currency.reqExp.ToString();
            requireValue.Text = "ใช้ดาวเพื่อปลดล็อค";
        }
       
        coinText.Text = "ได้รับเหรียญเพิ่ม    +" + vegDatabase.currency.coin.ToString();
        if(vegDatabase.growthTime == 0.5f)
        {
            expText.Text = "ใช้เวลาปลูก  30 วินาที";
        } else
        {
            expText.Text = "ใช้เวลาปลูก  " + vegDatabase.growthTime.ToString() + "  นาที";
        }
      
       // vegexpText.Text = "ได้รับค่าผักเพิ่ม    +" + vegDatabase.vegExpDrop.ToString();
       


    }



    public void Bought() {
    
        if (CheckMoneyValue())
        {
           
            soundManager.PlaySFX("Click");
            var cancel = new AlertPopup.ActionButton("ยกเลิก", null, new Color(0.9f, 0.9f, 0.9f));
            var ok = new AlertPopup.ActionButton("ตกลง", () => {
                vegController.BuyVeg(this);
            }, new Color(0f, 0.9f, 0.9f));
           
            AlertPopup.ActionButton[] buttons = { cancel,ok };
            GameManager.Ins.uiManager.alertPopup.ShowDialog("ต้องการซื้อ", desc_lang.name, buttons);

        } else
        {

            var ok = new AlertPopup.ActionButton("ตกลง", null, new Color(0.9f, 0.9f, 0.9f));
            AlertPopup.ActionButton[] buttons = { ok };
            GameManager.Ins.uiManager.alertPopup.ShowDialog("", "มีเหรียญไม่พอ", buttons);
        }

    }


    bool CheckMoneyValue()
    {
        bool status = false;

         status = (saveManager.data.coin >= vegDatabase.currency.price);

        return status;
    }

    bool CheckExpValue() {
        bool status = false;
       
              // status = (saveManager.data.coin >= vegDatabase.currency.price);
           
        status = (saveManager.data.exp >= vegDatabase.currency.reqExp);
         
        return status;
    }

}
