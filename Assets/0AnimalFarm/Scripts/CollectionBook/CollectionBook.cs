using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CollectionBook : MonoBehaviour
{
    [HideInInspector]
    public SaveManager save;
    [HideInInspector]
    public GameManager gameManager;
    public GameObject panel;

    [Header("Guest")]
    public Transform contentPoint;
    public GameObject charSlotPrefab;
    CharacterList characterList;
    List<CharSlot> guestSlots = new List<CharSlot>();
    List<CharSlot> villagerSlots = new List<CharSlot>();

    [Header("Guest Preview")]
    public GameObject characterPreview;
    public Image charPic;
    public Image charPicLock;
    public LanguageSelector nameChar;
    public LanguageSelector hint;
    public LanguageSelector descText;

    [Header("Villager Preview")]
    public Transform contentVill;
    public GameObject VillagerPreview;
    public Image villagerPic;
    public Image villagerLock;
    public LanguageSelector namevillager;
    public LanguageSelector descvillager;

    [Header("Postcard Preview")]
    public Image postImg;
    public LanguageSelector descPostcard;
    public Transform contentCard;
    public GameObject cardSlotPrefab;
    List<PostcardSlot> cardSlots = new List<PostcardSlot>();


    string currentPostcard = "00";
    string currentGuest = "00";
    string currentVillager = "00";
  

    [Header("Tab Group")]
    public List<RectTransform> tabAll;
    public List<GameObject> groupBook;
    public List<GameObject> groupPreview;
    private void Awake() {
        gameManager = GameManager.Ins;
        save = SaveManager.Ins;
    }

    private void Start() {
        panel.SetActive(false);
        
    }
    public void GetDatabase() {

        panel.SetActive(true);
       // panel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        if (gameManager == null) gameManager = GameManager.Ins;
        if (save == null) save = SaveManager.Ins;
        if (characterList == null) characterList = gameManager.database.characterList;
        SelectTab(0);
    }

    public void ClosePanel() {
        GameManager.Ins.isOpenMenu = false;
        foreach (Transform t in contentVill) {
            Destroy(t.gameObject);
        }

        foreach (Transform t in contentPoint) {
            Destroy(t.gameObject);
        }

        foreach (Transform t in contentCard)
        {
            Destroy(t.gameObject);
        }
        groupBook[0].SetActive(false);
        groupPreview[0].SetActive(false);

        groupBook[1].SetActive(false);
        groupPreview[1].SetActive(false);

        groupBook[2].SetActive(false);
        groupPreview[2].SetActive(false);

        currentTab = null;
        guestSlots.Clear();
        villagerSlots.Clear();
        cardSlots.Clear();
        currentIndex = -1;
        currentGuest = "00";
        currentVillager = "00";
        currentPostcard = "00";
        tabAll[0].DOAnchorPosY(663, 0.2f).SetEase(easeDown);
        tabAll[1].DOAnchorPosY(663, 0.2f).SetEase(easeDown);
        tabAll[2].DOAnchorPosY(663, 0.2f).SetEase(easeDown);
        panel.SetActive(false);
       
    }

    void CreateListVillager() {
        foreach (Transform t in contentVill) {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < characterList.villgers.Count; i++) {

            CharSlot charSlot = Instantiate(charSlotPrefab, contentVill, false).GetComponent<CharSlot>();
            charSlot.collectionBook = this;
            charSlot.isGuest = false;
            charSlot.customer = characterList.villgers[i];
            charSlot.ShowGuest();
            villagerSlots.Add(charSlot);
        }
    }

    void CreateListGuest() {

        foreach(Transform t in contentPoint) {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < characterList.guests.Count; i++) {

            CharSlot charSlot = Instantiate(charSlotPrefab, contentPoint, false).GetComponent<CharSlot>();
            charSlot.collectionBook = this;
            charSlot.isGuest = true;
            charSlot.customer = characterList.guests[i];
            charSlot.ShowGuest();
            guestSlots.Add(charSlot);
        }
    }

    void CreateListPostCard()
    {

      
        for (int i = 0; i < gameManager.database.postcards.Count; i++)
        {

            PostcardSlot cardSlot = Instantiate(cardSlotPrefab, contentCard, false).GetComponent<PostcardSlot>();
            cardSlot.collectionBook = this;
            cardSlot.postcard = gameManager.database.postcards[i];
            cardSlot.ShowPostcard();
            cardSlots.Add(cardSlot);
        }
    }


    public void ShowDetailText(string _hint, DescriptionData data, Sprite img, string _name, bool status) {
        SoundManager.Ins.PlaySFX("Click");
        if (!status) {
            hint.gameObject.SetActive(true);
            if (!currentGuest.Equals(_name)) {
                charPic.gameObject.SetActive(false);
                charPicLock.gameObject.SetActive(false);
              
                nameChar.Text = "???";
                descText.Text = "?????";
                hint.Text = _hint;
                charPicLock.sprite = img;
                charPicLock.gameObject.SetActive(true);
                currentGuest = _name;
            }
        } else {
           
            if (!currentGuest.Equals(_name)) {
                charPic.gameObject.SetActive(false);
                charPicLock.gameObject.SetActive(false);
                nameChar.Text = data.name;
                descText.Text = data.desc;
                charPic.sprite = img;
                charPic.gameObject.SetActive(true);

                currentGuest = _name;
             }
         }
        

        charPic.SetNativeSize();
        charPicLock.SetNativeSize();

    }

    public void ShowDetailVillager(DescriptionData data, Sprite img, string _name, bool status) {
        SoundManager.Ins.PlaySFX("Click");
        hint.gameObject.SetActive(false);
        if (!status) {

            if (!currentVillager.Equals(_name)) {
                villagerPic.gameObject.SetActive(false);
                villagerLock.gameObject.SetActive(false);
                namevillager.Text = "???";
                descvillager.Text = "?????";
                villagerLock.sprite = img;
                villagerLock.gameObject.SetActive(true);
                currentVillager = _name;
            }
        } else {

            if (!currentVillager.Equals(_name)) {
                villagerPic.gameObject.SetActive(false);
                villagerLock.gameObject.SetActive(false);
                namevillager.Text = data.name;
                descvillager.Text = data.desc;
                villagerPic.sprite = img;
                villagerPic.gameObject.SetActive(true);

                currentVillager = _name;
            }
        }


        villagerPic.SetNativeSize();
        villagerLock.SetNativeSize();

    }

    RectTransform currentTab;
    int currentIndex = -1;
    public Ease easeUp;
    public Ease easeDown;
    public void SelectTab(int index) {
        SoundManager.Ins.PlaySFX("Click");
        if (currentIndex == index) return;

        if (currentTab != null) {
            currentTab.DOAnchorPosY(663, 0.2f).SetEase(easeDown);
            groupBook[currentIndex].SetActive(false);
            groupPreview[currentIndex].SetActive(false);
        }

       
        currentIndex = index;
        currentTab = tabAll[index];
        groupPreview[index].SetActive(true);
        groupBook[index].SetActive(true);
        tabAll[index].DOAnchorPosY(705, 0.2f).SetEase(easeUp);

        if (index == 0){
            CreateListGuest();
        }
        else if (index == 1){
            CreateListVillager();
        }
        else if(index == 2){
            if (save.data.unlock_postcard.Count == 0)
            {
                groupPreview[index].SetActive(false);
            }

            if (cardSlots.Count == 0)
            {
              
                CreateListPostCard();
            }
        }
       
        
    }

    public void ShowPostcard(DescriptionData _data, Sprite img, string _name)
    {
        if (!currentPostcard.Equals(_name))
        {
            descPostcard.Text = _data.desc;
            postImg.sprite = img;
            postImg.SetNativeSize();
            currentPostcard = _name;
        }

    }




}
