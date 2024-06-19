using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class MarketControl : MonoBehaviour
{

    public GameObject slotStorage;
    public GameObject panel;
    public Transform content;
    public List<Inventory> inventories = new List<Inventory>();
    public TextMeshProUGUI allPrice;
    public TextMeshProUGUI total;
    public Image product;
    SaveManager save;
    [HideInInspector]
    public GameManager gm;

    public RectTransform groupSell;
    public RectTransform groupCart;
    public Ease easeIn;
    public Ease easeOut;
    public float timeMove;
    public float timeMoveCart;

    int countSell = 1;
    int countPrice = 0;

    Inventory inventory;
    VegDatabase vegDatabase;
    SlotMarket slotMarket;
    Button currentBtn;

    public GameObject minBtn;
    public GameObject maxBtn;
    void Start() {
        gm = GameManager.Ins;
        save = SaveManager.Ins;
      
        panel.SetActive(false);
        minBtn.SetActive(false);
        maxBtn.SetActive(false);
    }

    public void CreateMarket() {

        inventories.Clear();
        if (gm == null) gm = GameManager.Ins;
        if (save == null) save = SaveManager.Ins;
       
        if (slotMarket != null) {
            ClearData();
        }


        foreach (Transform t in content) {
            Destroy(t.gameObject);
        }
        inventories = save.data.inventory.Select(d => d.Value).ToList();

        for (int i = 0; i < inventories.Count; i++) {

            SlotMarket shop = Instantiate(slotStorage, content, false).GetComponent<SlotMarket>();

            shop.Getdatabase(this, inventories[i]);

        }


    }

    public void ClosePanel() {

        if (content.childCount > 0) {
            foreach (Transform t in content) {
                Destroy(t.gameObject);
            }
        }
        Vector2 pos = groupCart.position;
       if (pos.x <= 0) {
            gm.isOpenMenu = false;
            panel.SetActive(false);
        } else {
            groupSell.DOAnchorPosX(1080f, timeMove).SetEase(easeIn);
            groupCart.DOAnchorPosX(0f, timeMoveCart).SetEase(easeIn).OnComplete(() => {
                gm.isOpenMenu = false;
                panel.SetActive(false);
            });


        }

    }

  
    public void SelectCart(SlotMarket _slotMarket) {
        SoundManager.Ins.PlaySFX("Select");
        maxBtn.SetActive(true);
        if (slotMarket != null) {
            slotMarket.btn.interactable = true;
        } else {
            MoveMarketIn();
        }
        slotMarket = _slotMarket;
        currentBtn = slotMarket.btn;
        inventory = slotMarket.data;
        vegDatabase = slotMarket.vegDatabase;

        product.enabled = true;

        product.sprite = vegDatabase.product;
        product.SetNativeSize();

        CalculateAll();
    }

    void CalculateAll() {

        total.text = countSell + "/" + inventory.total;
        countPrice = vegDatabase.currency.coin * countSell;
        allPrice.text = countPrice.ToString();
    }


    public void AddProduct() {

        SoundManager.Ins.PlaySFX("Click");

        countSell++;
        if (countSell >= inventory.total) {
            countSell = inventory.total;
            minBtn.SetActive(true);
            maxBtn.SetActive(false);
        } else {
            minBtn.SetActive(true);
            maxBtn.SetActive(true);
        }

        CalculateAll();
    }

    public void RemoveProduct() {
        SoundManager.Ins.PlaySFX("Click");
        countSell--;
        if (countSell <= 1) {
            countSell = 1;
            minBtn.SetActive(false);
            maxBtn.SetActive(true);
        } else {
            minBtn.SetActive(true);
            maxBtn.SetActive(true);
        }

        CalculateAll();
    }

    public void Sell() {

        if (countPrice == 0) return;

        SoundManager.Ins.PlaySFX("Cash");

        gm.uiManager.money.AddCoin(countPrice);
        inventory.total = inventory.total - countSell;
        save.SaveInventory(inventory.idname,inventory,false);
        CreateMarket();
    }

    void ClearData() {
        if (slotMarket != null) {
            slotMarket.btn.interactable = true;
            slotMarket = null;
        }
        countPrice = 0;
        countSell = 1;
        total.text = "0/0";
        allPrice.text = "0";
       
        product.enabled = false;
    }
    public void NoSell() {
        MoveMarketOut();
        ClearData();
    }


    public void MoveMarketIn() {
        groupSell.DOAnchorPosX(235f,timeMove).SetEase(easeIn);
        groupCart.DOAnchorPosX(-200f, timeMoveCart).SetEase(easeOut);
    }

    public void MoveMarketOut() {
        groupSell.DOAnchorPosX(1080f, timeMove).SetEase(easeIn);
        groupCart.DOAnchorPosX(0f, timeMoveCart).SetEase(easeOut);
    }
}
