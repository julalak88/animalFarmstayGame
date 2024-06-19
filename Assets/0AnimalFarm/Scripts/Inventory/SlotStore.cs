using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static DialogTest;
using System.Security.Cryptography;

public class SlotStore : MonoBehaviour
{
    public LanguageSelector storeName;
    public Image product;
    public TextMeshProUGUI total;
    public Inventory data;
    public VegDatabase vegDatabase;
    [HideInInspector]
    public InventoryController invent;
    DescriptionData desc_lang;
    void Start()
    {
    }

    public void Getdatabase(InventoryController _invent, Inventory _veg)
    {
        invent = _invent;
        data = _veg;
        Show();
    }

    public void Show()
    {
        vegDatabase = invent.gm.database.vegetables.Select(v => v).Where(p => p.id.Equals(data.idname)).FirstOrDefault();
        desc_lang = vegDatabase.GetType().GetField(SaveManager.Ins.lang).GetValue(vegDatabase) as DescriptionData;
        storeName.Text = desc_lang.name;
        total.text = "X " + data.total.ToString();
        product.sprite = vegDatabase.product;
        product.SetNativeSize();
    }
     
}
