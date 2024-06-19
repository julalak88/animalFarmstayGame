using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CategoryCharacter : MonoBehaviour
{
    public Transform contentPoint;
    public Transform parentPoint;
    public GameObject charSlotPrefab;
    [HideInInspector]
    public SaveManager save;
    [HideInInspector]
    public GameManager gameManager;
    public GameObject detailPrefab;
    CharacterDetail detail;
    CharacterList characterList;
    List<CharSlot> charSlots = new List<CharSlot>();
    private void Awake() {
        gameManager = GameManager.Ins;
        save = SaveManager.Ins;
    }
    public void GetDatabase() {
        if(gameManager == null) gameManager = GameManager.Ins;
        if (save == null) save = SaveManager.Ins;
        characterList = gameManager.database.characterList;
        if (charSlots.Count <= 0) {
            CreateList();
        }
    }

    void CreateList() {
        for (int i = 0; i < characterList.guests.Count; i++) {
            CharSlot charSlot = new CharSlot();
            charSlot = Instantiate(charSlotPrefab, contentPoint, false).GetComponent<CharSlot>();
           // charSlot.categoryCharacter = this;
            charSlot.customer = characterList.guests[i];
            charSlot.ShowGuest();
            charSlots.Add(charSlot);
        }
    }

    public void ShowDescription(CustomerDatabase customerDatabase,bool isUnlock) {
        detail = Instantiate(detailPrefab, parentPoint, false).GetComponent<CharacterDetail>() ;
        detail.categoryCharacter = this;
        detail.ShowDetail(customerDatabase, isUnlock);
    }
}
