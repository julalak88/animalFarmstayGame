using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using I2.Loc;
public class CheckBoxController : MonoBehaviour
{
    public LanguageSelector textDes;
    public Image fadeBg;
    public GameObject box;
    public float time;
    public float timeFade;
    public Ease scaleIn = Ease.Flash;
    public Ease scaleOut = Ease.Flash;
    public GameObject groupButton;
    public Button accept;
    public Button cancel;
    SaveManager saveManager;
    SoundManager soundManager;
    string lang;
    private void OnEnable() {
       
        groupButton.SetActive(false);
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, timeFade);
        box.transform.localScale = Vector3.zero;
        box.transform.DOScale(1, time).SetEase(scaleIn);
    }

    public void ShowBox(bool money1,bool money2) {
        if (soundManager == null) soundManager = SoundManager.Ins;
        soundManager.PlaySFX("Pop");
        if (!saveManager) saveManager = SaveManager.Ins;
        lang = saveManager.lang;

        string msg_money = LocalizationManager.GetTranslation("Other/not_enough", true, 0, true, false, null, lang);
        string msg_candy = LocalizationManager.GetTranslation("Other/not_enough_candy", true, 0, true, false, null, lang);
        string msg_topup = LocalizationManager.GetTranslation("Other/topup", true, 0, true, false, null, lang);
        if ((money1 && !money2) || !money1 && !money2) {
            textDes.Text = msg_candy;
            box.transform.DOScale(0, time).SetEase(scaleOut).SetDelay(1.5f).OnComplete(ClosePanel);
        } else if(!money1 && money2) {
            groupButton.SetActive(true);
            textDes.Text = msg_money + " \n" + msg_topup;
        }
    }

    public void Cancel() {
        soundManager.PlaySFX("Click2");
        box.transform.DOScale(0, time).SetEase(scaleOut).OnComplete(ClosePanel);
    }

    public void ClosePanel() {
        groupButton.SetActive(false);
        gameObject.SetActive(false);
    }
}
