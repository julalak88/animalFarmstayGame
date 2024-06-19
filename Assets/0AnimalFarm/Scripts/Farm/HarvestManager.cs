using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
public class HarvestManager : SerializedMonoBehaviour
{
    public Dictionary<string, GameObject> products;
    public Transform target;
    string productId;
    SaveManager saveManager;
    GameManager gm;
    VegDatabase veg;
    private void Start() {
        saveManager = SaveManager.Ins;
        gm = GameManager.Ins;
    }

    public void CreateProduct(string productName, Character customer, Vector3 position, int _coin, float _exp, float _vegExp) {
        GameObject obj = products[productName];
        Product product = Instantiate(obj).GetComponent<Product>();
        product.name = productName;
      //  product.customer = customer;
        product.coin = _coin;
        product.exp = _exp;
        product.vegExp = _vegExp;
        product.transform.position = position;
    }
 
  
    public void CreateProduct(string productName,Vector3 position)//for player
    {
      
        GameObject obj = products[productName];
        Product product = Instantiate(obj).GetComponent<Product>();
        product.name = productName;
        product.target = target;
        product.transform.position = position;

        productId = productName;
        UpdateInventory();
    }

    void UpdateInventory()
    {
        if(saveManager==null) saveManager = SaveManager.Ins;
        if (gm == null) gm = GameManager.Ins;

        veg = gm.database.vegetables.Select(v => v).Where(p => p.name.Equals(productId)).FirstOrDefault();
        Inventory inventory = new Inventory();
        inventory.idname = productId;
        inventory.total = 1;
        inventory.plantType = veg.plantType;
         saveManager.SaveInventory(productId, inventory);
       
    }

    
}
