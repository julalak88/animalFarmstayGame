using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using I2.Loc;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using DG.Tweening.Core.Easing;

public class FoodSlot : MonoBehaviour
{
    public LanguageSelector foodName;
    public GameObject vegRecipe;
    public Image pic;
    public Image picLock;
    public Transform groupRecipe;
    [HideInInspector]
    public FoodController foodController;
    [HideInInspector]
    public FoodDatabase foodDatabase;
    public GameObject foodOwner;
    public Button buyBtn;
    public GameObject ingredientsGroup;
    [Header("Textmeshpro")]
    public LanguageSelector coinText;
    DescriptionData desc_lang;
    [HideInInspector]
    public bool isUnlock = false;
    public float time;
    public Ease scaleIn = Ease.Flash;
    SaveManager saveManager;
    SoundManager soundManager;
    string lang;
   

    private void Awake()
    {
        saveManager = SaveManager.Ins;
        soundManager = SoundManager.Ins;
        pic.gameObject.SetActive(false);
        picLock.gameObject.SetActive(false);
        foodOwner.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        ingredientsGroup.SetActive(false);
    }

    private void Start()
    {
     

    }
    public VegDatabase veg;
    public void Show()
    {

        if (!saveManager) saveManager = SaveManager.Ins;
        lang = saveManager.lang;

        desc_lang = foodDatabase.GetType().GetField(lang).GetValue(foodDatabase) as DescriptionData;
        string owner = "";

         owner = saveManager.data.bought_food.Select(f => f).Where(t => t.Equals(foodDatabase.id)).SingleOrDefault();
        
      
       
         isUnlock = GameManager.Ins.unlockManager.CheckUnlockFood(foodDatabase.id);
      
        if (!string.IsNullOrEmpty(owner))
        {
            foodName.Text = desc_lang.name;
            pic.gameObject.SetActive(true);
            if (pic.sprite == null) pic.sprite = foodDatabase.image;
            pic.SetNativeSize();
            foodOwner.SetActive(true);
            buyBtn.gameObject.SetActive(false);
            ingredientsGroup.SetActive(false);

        } else
        {
            
            if (isUnlock)
            {
                foodName.Text = desc_lang.name;
                pic.gameObject.SetActive(true);
                if (pic.sprite == null) pic.sprite = foodDatabase.image;
                pic.SetNativeSize();
                foodOwner.SetActive(false);
                buyBtn.gameObject.SetActive(true);
                buyBtn.interactable = true;
                ingredientsGroup.SetActive(true);
            } else
            {
                foodName.Text = "?????";
                picLock.gameObject.SetActive(true);
                if (picLock.sprite == null) picLock.sprite = foodDatabase.image;
                picLock.SetNativeSize();
                foodOwner.SetActive(false);
                buyBtn.gameObject.SetActive(true);
                buyBtn.interactable = false;
                ingredientsGroup.SetActive(true);
            }

            for (int i = 0; i < foodDatabase.ingredients.Count; i++)
            {
                
                veg = GameManager.Ins.database.vegetables.Select(v => v).Where(p => p.id.Equals(foodDatabase.ingredients[i].itemName)).FirstOrDefault();
                VegRecipe recipe = Instantiate(vegRecipe, groupRecipe, false).GetComponent<VegRecipe>();
                recipe.Show(veg.product, foodDatabase.ingredients[i].count);
            }


        }


        coinText.Text = "ได้รับเหรียญเพิ่ม    +" + foodDatabase.currency.coin.ToString();
    
    }



    public void Bought()
    {

        if (CheckValueFood())
        {
            
            soundManager.PlaySFX("Click");
            var cancel = new AlertPopup.ActionButton("ยกเลิก", null, new Color(0.9f, 0.9f, 0.9f));
            var ok = new AlertPopup.ActionButton("ตกลง", () => {
               foodController.BuyFood(this);
            }, new Color(0f, 0.9f, 0.9f));

            AlertPopup.ActionButton[] buttons = { cancel, ok };
            GameManager.Ins.uiManager.alertPopup.ShowDialog("เรียนรู้", desc_lang.name, buttons);

        } else
        {

            var ok = new AlertPopup.ActionButton("ตกลง", null, new Color(0.9f, 0.9f, 0.9f));
            AlertPopup.ActionButton[] buttons = { ok };
            GameManager.Ins.uiManager.alertPopup.ShowDialog("", "วัตถุดิบไม่พอ", buttons);
        }

    }


    bool CheckValueFood()
    {
        bool status = false;

        status = GameManager.Ins.unlockManager.CheckFoodCanLearn(foodDatabase.ingredients);

        return status;
    }

   

}
