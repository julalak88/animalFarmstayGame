using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Linq;
 
using static DialogTest;


public class RewardController : MonoBehaviour
{
    [Header("UI")]
    public RectTransform headerBoard;
    public GameObject panelParent;
    public GameObject panelGroup;
    public GameObject paper;
    [Header("Postcard")]
    public Image poscardImage;
    [Header("Veg")]
    public Image vegImage;
    [Header("Food")]
    public Image foodImage;
    public LanguageSelector desc;
    public LanguageSelector nameText;
    public LanguageSelector textHead;
    public Button share;
    public Button closeBtn;
   // [HideInInspector]
    public string currentFunction;
    [SerializeField]
    GameManager gm;
      [SerializeField]
    SaveManager save;
    private void Start() {

        gm = GameManager.Ins;
        save = SaveManager.Ins;

        headerBoard.localPosition = new Vector3(0,1500,0);
        poscardImage.transform.localScale = Vector3.zero;
        vegImage.transform.localScale = Vector3.zero;
        foodImage.transform.localScale = Vector3.zero;
        share.transform.localScale = Vector3.zero;
        closeBtn.transform.localScale = Vector3.zero;
        nameText.gameObject.transform.localScale = Vector3.zero;
        panelGroup.SetActive(false);
        panelParent.SetActive(false);
    }
    void MoveOut()
    {
       
        closeBtn.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        paper.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        if (currentFunction.Equals("postcard"))
        {
            poscardImage.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

           
        } else if (currentFunction.Equals("veg") || currentFunction.Equals("food"))
        {
            vegImage.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
            foodImage.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
            nameText.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        }

        share.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => panelGroup.SetActive(false));

        headerBoard.DOAnchorPosY(1500f, 1f).SetEase(Ease.InBack).OnComplete(() => {
   

            if (currentFunction.Equals("postcard"))
            {
                poscardImage.sprite = null;

            } else if (currentFunction.Equals("veg"))
            {
                vegImage.sprite = null;

            }else if (currentFunction.Equals("food"))
            {
                foodImage.sprite = null;
            }
                panelParent.SetActive(false);
        });
       
    }

    string currentId;
    public void ShowReward(string _id) {
        gm = GameManager.Ins;
        save = SaveManager.Ins;
        currentId = _id;
        DescriptionData desc_lang;
        if (currentFunction.Equals("postcard"))
        {
            textHead.Text = "ความทรงจำ";
            
            PostcardDatabase postcard = gm.database.postcards.Select(v => v).Where(p => p.id.Equals(_id)).FirstOrDefault();
            desc_lang = postcard.GetType().GetField(save.lang).GetValue(postcard) as DescriptionData;
            gm.unlockManager.UnlockPostcard(_id);
           
            poscardImage.sprite = postcard.image;
            poscardImage.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.25f).SetEase(Ease.OutBack);
            desc.Text = desc_lang.desc;
        }else if (currentFunction.Equals("veg"))
        {

            textHead.Text = "เมล็ดพืช";
            vegImage.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 0.5f).SetDelay(0.25f).SetEase(Ease.OutBack);
            nameText.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.25f).SetEase(Ease.OutBack);
            desc.gameObject.SetActive(false);

            VegDatabase veg = gm.database.vegetables.Select(v => v).Where(p => p.id.Equals(_id)).FirstOrDefault();
            vegImage.sprite = veg.thumbnail;
            vegImage.SetNativeSize();
            desc_lang = veg.GetType().GetField(save.lang).GetValue(veg) as DescriptionData;
            nameText.Text = desc_lang.name;
        }else if (currentFunction.Equals("food"))
        {
            textHead.Text = "อาหารแสนอร่อย";
            foodImage.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetDelay(0.25f).SetEase(Ease.OutBack);
            nameText.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.25f).SetEase(Ease.OutBack);
            desc.gameObject.SetActive(false);

            FoodDatabase food = gm.database.foods.Select(v => v).Where(p => p.id.Equals(_id)).FirstOrDefault();
            foodImage.sprite = food.image;
            foodImage.SetNativeSize();
            desc_lang = food.GetType().GetField(save.lang).GetValue(food) as DescriptionData;
            nameText.Text = desc_lang.name;
        }


        panelGroup.SetActive(true);
        headerBoard.DOAnchorPosY(655f,1f).SetEase(Ease.OutBack);
        paper.transform.DOScale(Vector3.one,0.5f).SetDelay(0.15f).SetEase(Ease.OutBack);
        share.transform.DOScale(Vector3.one,0.5f).SetDelay(0.3f).SetEase(Ease.OutBack);
        closeBtn.transform.DOScale(Vector3.one,0.5f).SetDelay(0.35f).SetEase(Ease.OutBack);
    }



    public void ClosePanel() {
       
        desc.Text = string.Empty;
        nameText.Text = string.Empty;
        MoveOut();

    }
   
}
