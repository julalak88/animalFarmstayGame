using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using unityad;

public class VegController : MonoBehaviour
{
    public GameObject vegSlotPrefab;
    public Transform contentPoint;
    public Scrollbar scrollbar;
    public RectTransform panel;
    [HideInInspector]
    public List<VegSlot> slots;
    [HideInInspector]
    public Plot plot;
    public float time;
    public Ease moveIn = Ease.Flash;
    public Ease moveOut = Ease.Flash;
    public LanguageSelector textHead;
    public Image fadeBg;
    SaveManager saveManager;
    GameManager gameManager;
    SoundManager soundManager;

    private void OnEnable() {
        gameManager = GameManager.Ins;
        GameManager.Ins.uiManager.alertPopup.SetRaycasts(true);
    }

   public void Show(Plot plot = null) {
        foreach (Transform t in contentPoint)
        {
            Destroy(t.gameObject);
        }

        if (soundManager == null) soundManager = SoundManager.Ins;
        if (gameManager == null) gameManager = GameManager.Ins;

        if (saveManager == null) saveManager = SaveManager.Ins;
        if (plot != null)
        {

            this.plot = plot;
        }

        //  if (slots.Count <= 0) {
        for (int i = 0; i < gameManager.database.vegetables.Count; i++) {
                VegSlot slot;
                slot = Instantiate(vegSlotPrefab, contentPoint, false).GetComponent<VegSlot>();
                slot.vegController = this;
                slot.vegDatabase = gameManager.database.vegetables[i];
                slot.Show();
                slots.Add(slot);
            }
      //  } else {
           // slots.Select(c => c).ToList().ForEach(cc => cc.Show());
       // }
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, time);
        panel.DOAnchorPosY(0, time).SetEase(moveIn);
        textHead.Text = "เมล็ดพันธุ์";

    }
  
    public void BuyVeg(VegSlot vegSlot) {
             gameManager.uiManager.panelUIManager.questController.UpdateProgress(1, QuestAction.GROW);
             plot.Plant(vegSlot.vegDatabase.name);
            gameManager.uiManager.money.RemoveCoin(vegSlot.vegDatabase.currency.price);
            Vector2 pos = panel.transform.localPosition;
            panel.transform.localPosition = new Vector2(pos.x, 1950);
            fadeBg.DOFade(0, 0.01f);
            scrollbar.value = 1;
            plot = null;
             gameManager.uiManager.alertPopup.SetRaycasts(false);
            
           gameObject.SetActive(false);
        
     }
      
    
    public void ClosePanel() {
        fadeBg.DOFade(0, time);
        panel.DOAnchorPosY(1950, time).SetEase(moveOut).OnComplete(() => {
           
            plot = null;
            scrollbar.value = 1;
            gameManager.uiManager.alertPopup.SetRaycasts(false);
            gameObject.SetActive(false);
        });
    }

}
