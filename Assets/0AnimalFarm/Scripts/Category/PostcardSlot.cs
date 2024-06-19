using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
public class PostcardSlot : MonoBehaviour
{

    public Image image;
    public GameObject keyLock;
    [HideInInspector]
    public CollectionBook collectionBook;
    [HideInInspector]
    public PostcardDatabase postcard;
    DescriptionData desc_lang;
    internal UnlockManager unlock;
    public Button btn;
    private void Start()
    {
        unlock = GameManager.Ins.unlockManager;
    }
    public void ShowPostcard()
    {
        if (unlock == null)
        {
            unlock = GameManager.Ins.unlockManager;
        }
        desc_lang = postcard.GetType().GetField(collectionBook.save.lang).GetValue(postcard) as DescriptionData;
        if (unlock.CheckUnlockPostcard(postcard.id))
        {
            keyLock.SetActive(false);
            image.sprite = postcard.image;
            image.SetNativeSize();
        }
        else
        {
            btn.interactable = false;
        }

        if (postcard.id.Equals("poc0000")) {
            Select();
        }
    }

    public void Select()
    {
        collectionBook.ShowPostcard(desc_lang, postcard.image, postcard.id);
    }
}
