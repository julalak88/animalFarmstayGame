using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using System.Linq;
public class InventoryController : MonoBehaviour
{
    public GameObject slotStorage;
    public GameObject panel;
    public Transform content;
    public List<Inventory> inventories = new List<Inventory>();
    SaveManager save;
    [HideInInspector]
    public GameManager gm;
 
    void Start()
    {
        gm = GameManager.Ins;
        save = SaveManager.Ins;
        panel.SetActive(false);
    }

     
    public void CreateInventory()
    {
        panel.SetActive(true);
        if (gm == null) gm = GameManager.Ins;
        if (save == null) save = SaveManager.Ins;

        inventories = save.data.inventory.Select(d => d.Value).ToList();

        for (int i = 0; i < inventories.Count; i++)
        {

            SlotStore vetSlot = Instantiate(slotStorage, content, false).GetComponent<SlotStore>();

            vetSlot.Getdatabase(this, inventories[i]);
             
        }
    }

    public void ClosePanel()
    {
        GameManager.Ins.isOpenMenu = false;
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }

        
        panel.SetActive(false);

    }

}
