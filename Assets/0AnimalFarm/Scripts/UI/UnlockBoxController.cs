using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using I2.Loc;

public class UnlockBoxController : MonoBehaviour
{ 
    public LanguageSelector textMsg;
    public Image fadeBg;
    public Image image;
    public Sprite plot;
    public GameObject box;
    public GameObject selectButton;
    public GameObject cancelButton;
    public float timeFade;
    public float time;
    public Ease scaleIn = Ease.Flash;
    ItemDatabase itemDatabase;
    AUpgrade aUpgrade;
    MiniMapController miniMap;
    SaveManager saveManager;
    GameManager gameManager;
    SoundManager soundManager;
    string lang;
    string not_enough;
    int plotPrice = 1000;
    private void Start() {
        saveManager = SaveManager.Ins;
        gameManager = GameManager.Ins;
        soundManager = SoundManager.Ins;
    }
    private void OnEnable() {
        if (soundManager == null) soundManager = SoundManager.Ins;
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, timeFade);
        box.transform.localScale = Vector3.zero;
        box.transform.DOScale(1, time).SetEase(scaleIn);
      
    }


    void GetLang() {
        if (!saveManager) saveManager = SaveManager.Ins;
        if (!soundManager) soundManager = SoundManager.Ins;

        lang = saveManager.lang;
        not_enough = LocalizationManager.GetTranslation("Other/not_enough", true, 0, true, false, null, lang);
    }
    public void ShowDataUnlock(ItemDatabase itemDatabase, AUpgrade aUpgrade) {
        GetLang();
      
        this.itemDatabase = itemDatabase;
        this.aUpgrade = aUpgrade;
        DescriptionData desc_lang = itemDatabase.GetType().GetField(lang).GetValue(itemDatabase) as DescriptionData;

        string msg1 = LocalizationManager.GetTranslation("Other/unlock", true, 0, true, false, null, lang);
        string msg2 = LocalizationManager.GetTranslation("Other/price", true, 0, true, false, null, lang);
        image.sprite = itemDatabase.image;
        string msg = "";
        if (CheckMoneyValue()) {
            cancelButton.SetActive(true);
            selectButton.SetActive(true);
            msg = msg1 + " <color=#008000ff> " + " \n" + desc_lang.name + "</color>" + "\n" + msg2 + " <color=#008000ff>" + itemDatabase.currency.price + "</color>";
        } else {
            selectButton.SetActive(false);
            cancelButton.SetActive(false);
            msg = not_enough;
        }
      
        textMsg.Text = msg;
    }

    public void ShowUnlockPlot(AUpgrade aUpgrade) {
        GetLang();
       
        this.aUpgrade = aUpgrade;
        string msg;
        string msg1 = LocalizationManager.GetTranslation("Other/unlock", true, 0, true, false, null, lang);
        string msg2 = LocalizationManager.GetTranslation("Farm/plot", true, 0, true, false, null, lang);
        string msg3 = LocalizationManager.GetTranslation("Other/price", true, 0, true, false, null, lang);
        image.sprite = plot;
        if (CheckMoneyValue()) {
            selectButton.SetActive(true);
            cancelButton.SetActive(true);
            msg = msg1 + "<color=#008000ff>" + "\n" + msg2 + "</color>" + "\n" + msg3 + " <color=#008000ff>" + plotPrice + "</color>";

        } else {
            cancelButton.SetActive(false);
            selectButton.SetActive(false);
            msg = not_enough;
        }
      
        textMsg.Text = msg;
    }

    public void ShowUnlockMiniMap(MiniMapController miniMap) {
        GetLang();
        this.miniMap = miniMap;
        DescriptionData desc_lang = miniMap.currentMap.location.GetType().GetField(lang).GetValue(miniMap.currentMap.location) as DescriptionData;
      
        string msg1 = LocalizationManager.GetTranslation("Other/unlock", true, 0, true, false, null, lang);
        string msg2 = LocalizationManager.GetTranslation("Other/price", true, 0, true, false, null, lang);
        image.sprite = miniMap.currentMap.location.image;
        string msg = "";
        if (CheckMoneyValue()) {
            cancelButton.SetActive(true);
            selectButton.SetActive(true);
            msg = msg1 + "<color=#008000ff> " + " \n" + desc_lang.name + "</color>" + "\n" + msg2 + " <color=#008000ff>" + miniMap.currentMap.location.price + "</color>";
          
        } else {
            selectButton.SetActive(false);
            cancelButton.SetActive(false);
            msg = not_enough;
        }

        textMsg.Text = msg;
    }

    
    public void HideUnlockBox() {
        soundManager.PlaySFX("Click2");
        image.sprite = null;
        fadeBg.DOFade(0, timeFade);
        gameObject.SetActive(false);
    }

    public bool CheckMoneyValue() {
        bool status = false;
        if (aUpgrade is ObjectUpgrade) {
            status = (saveManager.data.coin >= itemDatabase.currency.price);
        } else if(aUpgrade is AUpgrade) {
            status = (saveManager.data.coin >= plotPrice);
        } else {
            status = (saveManager.data.coin >= miniMap.currentMap.location.price);
        }
        return status;
    }

    public void Select() {
        soundManager.PlaySFX("NewItem");
        if (!saveManager) gameManager = GameManager.Ins;

       string currentScene = gameManager.sceneManager.sceneChanger.currentScene.name;
      
        if (aUpgrade is ObjectUpgrade) {
            saveManager.SaveItem(itemDatabase, currentScene, aUpgrade.name);
            gameManager.uiManager.money.RemoveCoin(itemDatabase.currency.price);
    
            ((ObjectUpgrade)aUpgrade).LoadObject(currentScene, itemDatabase.name, true,true);
        } else if (aUpgrade is AUpgrade) {
            saveManager.SavePlot(aUpgrade.name);
            Plot plot = ((Plot)aUpgrade);
            gameManager.uiManager.money.RemoveCoin(plotPrice);
            PlotData plotData = saveManager.data.plots[aUpgrade.name];
            plot.LoadObject(plotData);
            Destroy(plot.item);

        } else {
            saveManager.SaveScene(miniMap.currentMap.name);
            miniMap.SetOnUnlock();
        }
        image.sprite = null;
        gameObject.SetActive(false);
    }

    
}
