using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using DG.Tweening;
using System.Security.Claims;

public class QuestController : MonoBehaviour
{
    public GameObject panel;
    public GameObject fadeBg;
    [Header("QuestTutorial")]
    public List<QuestDatabase> quests;
    [Header("UI")]
    public Image questImg;
    public Image rewardImg;
    public TextMeshProUGUI progressText;
    public LanguageSelector textDescA;
    public LanguageSelector textDescB;
    public LanguageSelector textReward;
    public TextMeshProUGUI valueReward;
    public Image check;
    public GameObject noti;
   // [HideInInspector]
    public QuestData questData;
   // [HideInInspector]
    public QuestDatabase currentQuest;
    public GameObject coinEffect;
    DescriptionData desc_lang;
    SaveManager saveManager;
    GameManager gm;
    string lang;
    string[] messages;
    bool canClose;
    bool isClaim;
    void Start()
    {
        panel.transform.localScale = Vector3.zero;
        check.fillAmount = 0;
        canClose = true;
        isClaim = false;
        fadeBg.SetActive(false);
        noti.SetActive(false);
        
       
    }


    public void SetQuest(string questId, QuestData _questData = null)
    {
        if (!saveManager) saveManager = SaveManager.Ins;
        if (!gm) gm = GameManager.Ins;
        lang = saveManager.lang;

        currentQuest = quests.Select(v => v).Where(p => p.id.Equals(questId)).FirstOrDefault();
        desc_lang = currentQuest.GetType().GetField(lang).GetValue(currentQuest) as DescriptionData;

        if (_questData == null)
        {
            questData.questId = questId;
            questData.progress = 0;
            questData.complete = false;
        } else
        {
            questData.questId = _questData.questId;
            questData.progress = _questData.progress;
            questData.complete = false;
        }

        isClaim = false;

    }

    public void ShowUIQuest()
    {
        if (currentQuest == null) return;

        if (isClaim) return;


        check.fillAmount = 0;
        fadeBg.SetActive(true);
       // panel.SetActive(true);
        panel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        questImg.sprite = currentQuest.image;
        questImg.SetNativeSize();
        messages = desc_lang.desc.Split(new string[] { "&" }, StringSplitOptions.None);
        print("messages : "+messages[0]);
        textDescA.Text = messages[0];
        if (messages.Length > 1)
        {
            textDescB.Text = messages[1];
        } else
        {
            textDescB.gameObject.SetActive(false);
        }

        progressText.text = (questData.progress + " / " + currentQuest.total).ToString();
        textReward.Text = "รางวัล";
        rewardImg.sprite = currentQuest.rewardImg;
        rewardImg.SetNativeSize();
        valueReward.text = " + " + currentQuest.coinReward.ToString();

    }




    public void UpdateProgress(int count, QuestAction quest)
    {
         if(currentQuest==null) return;

        if (isClaim) return;



        if (quest.Equals(currentQuest.action))
        {
  
            if (currentQuest.action.Equals(QuestAction.COLLECT))
            {
             
                if (!questData.complete)
                {
                    questData.progress += count;
                }
               
                if (questData.progress >= currentQuest.total)
                {
                    questData.complete = true;
                    ClaimReward();


                }
            } else if (currentQuest.action.Equals(QuestAction.GROW))
            {
              
                if (!questData.complete)
                {
                    questData.progress += count;
                }

                if (questData.progress >= currentQuest.total)
                {
                    questData.complete = true;
                    ClaimReward();
                }
            }
        }


    }



    void ClaimReward()
    {
        canClose = false;
        ShowUIQuest();
      
        check.DOFillAmount(1,0.5f).OnComplete(() =>
        {
            
            isClaim = true;
            coinEffect.SetActive(true);
            gm.uiManager.money.AddCoin(currentQuest.coinReward);
            canClose = true;
           // currentQuest = null;
        });

    }


    void MoveOut()
    {
         
        panel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
           
            coinEffect.SetActive(false);
            fadeBg.SetActive(false);
           
        });

    }

    public void ClosPanel()
    {
        if (canClose)
        {
            MoveOut();
        }
    }


}
