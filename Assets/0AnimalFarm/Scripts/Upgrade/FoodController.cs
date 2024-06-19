using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class FoodController : MonoBehaviour
{
    public GameObject foodSlotPrefab;
    public Transform contentPoint;
    public Scrollbar scrollbar;
    public RectTransform panel;
 
    public float time;
    public Ease moveIn = Ease.Flash;
    public Ease moveOut = Ease.Flash;
    public LanguageSelector textHead;
    public Image fadeBg;
    SaveManager saveManager;
    GameManager gameManager;
    SoundManager soundManager;


    private void OnEnable()
    {
        gameManager = GameManager.Ins;
        GameManager.Ins.uiManager.alertPopup.SetRaycasts(true);
    }
    List<string> owner;
    public void Show() {

        foreach (Transform child in contentPoint)
        {
            Destroy(child.gameObject);
        }

        if (soundManager == null) soundManager = SoundManager.Ins;
        if (gameManager == null) gameManager = GameManager.Ins;

        if (saveManager == null) saveManager = SaveManager.Ins;
      
        gameManager.database.foods = gameManager.database.foods.OrderBy(x => x.vegExpReq).ToList();

        //if (slots.Count <= 0)
        //{
            for (int i = 0; i < gameManager.database.foods.Count; i++)
            {
                FoodSlot slot;
                slot = Instantiate(foodSlotPrefab, contentPoint, false).GetComponent<FoodSlot>();
                slot.foodController = this;
                slot.foodDatabase = gameManager.database.foods[i];
                slot.isUnlock = gameManager.unlockManager.CheckUnlockFood(slot.foodDatabase.name);
                if (slot.isUnlock)
                {
                    slot.transform.SetAsFirstSibling();
                }
            slot.Show();
               // slots.Add(slot);

                
            }
        //} else
        //{
        //    slots.Select(c => c).ToList().ForEach(cc => cc.Show());
        //}
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, time);
        panel.DOAnchorPosY(0, time).SetEase(moveIn);
        textHead.Text = "อาหารแสนอร่อย";

    }
    

    public void BuyFood(FoodSlot foodSlot)
    {
        RemoveVeg(foodSlot);
        if (!saveManager.data.bought_food.Contains(foodSlot.foodDatabase.name))
        {
            gameManager.uiManager.money.RemoveExpVeg(foodSlot.foodDatabase.vegExpReq);
         
            saveManager.SaveFood(foodSlot.foodDatabase.name);
        }
        gameManager.uiManager.alertPopup.SetRaycasts(false);
        
        scrollbar.value = 1;
        gameManager.uiManager.panelUIManager.ShowReward(foodSlot.foodDatabase.id, "food");
        gameObject.SetActive(false);

    }

    public void RemoveVeg(FoodSlot foodSlot )
    {
        for (int i = 0; i < foodSlot.foodDatabase.ingredients.Count; i++)
        {
           
            saveManager.RemoveInventory(foodSlot.foodDatabase.ingredients[i].itemName, foodSlot.foodDatabase.ingredients[i].count);

        }
    }


    public void ClosePanel() {
        fadeBg.DOFade(0, time);
        panel.DOAnchorPosY(1950, time).SetEase(moveOut).OnComplete(() => {
            foreach (Transform child in contentPoint) {
                Destroy(child.gameObject);
            }
            scrollbar.value = 1;
            gameManager.uiManager.alertPopup.SetRaycasts(false);
         
            gameObject.SetActive(false);
        });
    }
}
