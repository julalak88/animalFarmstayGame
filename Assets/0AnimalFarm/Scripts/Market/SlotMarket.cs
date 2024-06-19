using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using I2.Loc;
using TMPro;
using System.Linq;
public class SlotMarket : MonoBehaviour
{
    public LanguageSelector price;
    public LanguageSelector unit;
    public Image product;
   // [HideInInspector]
    public Inventory data;
  //  [HideInInspector]
    public VegDatabase vegDatabase;
    [HideInInspector]
    public MarketControl market;
    public Button btn;
    void Start()
    {
        
    }

    public void Getdatabase(MarketControl _market, Inventory _veg)
    {
        market = _market;
        data = _veg;
        Show();
    }

    public void Show()
    {
        vegDatabase = market.gm.database.vegetables.Select(v => v).Where(p => p.id.Equals(data.idname)).FirstOrDefault();
        unit.Text = "มีอยู่ " + data.total;
        price.Text = "ราคาต่อหน่วย " + vegDatabase.currency.coin.ToString();
        product.sprite = vegDatabase.product;
        product.SetNativeSize();
    }

    public void Select()
    {
        btn.interactable= false;
        market.SelectCart(this);
    }

}
