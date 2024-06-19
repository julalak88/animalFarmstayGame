using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharSlot : MonoBehaviour
{
    public LanguageSelector charName;
    [HideInInspector]
    public CollectionBook collectionBook;
 
    public CustomerDatabase customer;
    public Image imageLock;
    public Image image;
    public bool isUnlock;
    public bool isGuest = false;
    DescriptionData desc_lang;
    DialogDetail story;
    private void Awake() {
        imageLock.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
    }

    private void OnEnable() {
        //if(collectionBook != null) {
           
        //        ShowGuest();
            
        //}
    }

    public void ShowGuest() {
        desc_lang = customer.GetType().GetField(collectionBook.save.lang).GetValue(customer) as DescriptionData;
        story = customer.GetType().GetField("dialog" + SaveManager.Ins.lang).GetValue(customer) as DialogDetail;
        if (isGuest) {
            isUnlock = collectionBook.gameManager.unlockManager.CheckUnlockCustomer(customer.name);
            if (isUnlock) {
                charName.Text = desc_lang.name;
                image.gameObject.SetActive(true);
                image.sprite = customer.image;
                if (customer.id.Equals("gst0000")) {
                    Select();
                }

            } else {
                charName.Text = SetText();
                imageLock.gameObject.SetActive(true);
                imageLock.sprite = customer.image;
                if (customer.id.Equals("gst0000")) {
                    Select();
                }

            }
        } else {
            charName.Text = desc_lang.name;
            image.gameObject.SetActive(true);
            image.sprite = customer.image;
            if (customer.id.Equals("vil0000")) {
                Select();
            }
        }
      


    }

    string SetText() {
        return "???";
    }

    public void Select() {
        if (isGuest) {
            string hi = "-";
            if (!string.IsNullOrEmpty(story.hint)) {
                hi = story.hint;
            }
            if (isUnlock) {
                collectionBook.ShowDetailText(hi,desc_lang, customer.thumbnail, customer.id, isUnlock);
            } else {
                DescriptionData temp = new DescriptionData();
                temp.name = "-";
                temp.desc = "-";
              
                collectionBook.ShowDetailText(hi,temp, customer.thumbnail, customer.id, isUnlock);
            }
        } else {
            collectionBook.ShowDetailVillager(desc_lang, customer.thumbnail, customer.id, true);
        }
    }
}
