using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SettingController : MonoBehaviour
{
    public Image fadeBg;
    public Button closeBtn;
    public RectTransform panel;
    public float time;
    public Ease moveIn = Ease.Flash;
    public Ease moveOut = Ease.Flash;

    GameManager gameManager;
    SaveManager save;

    private void OnEnable() {

        if(gameManager == null) gameManager = GameManager.Ins;
        if (save == null) save = SaveManager.Ins;
        gameManager.uiManager.menu.settingBtn.onClick.RemoveAllListeners();
       
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, time);
        panel.DOAnchorPosY(0, time).SetEase(moveIn).OnComplete(() => {
            closeBtn.onClick.AddListener(ClosePanel);
        });

    }

    #region // -----------------------  clound saving  ---------------------------- //
    string loginType = "";
    public void SaveData() {
        save.uploadTime = DateTime.Now;
        save.data.stampTime = DateTime.Now;
        gameManager.SaveGame();
        save.SaveUploadTime();
        if (save.data != null) {
            if (string.IsNullOrEmpty(save.login)) {
                ShowLoginPopup();
            } else {
                gameManager.uiManager.loading.SetActive(true);
          
            }
        }
    }

    void onUploadSuccess() {
        save.onUploadSuccess.RemoveListener(onUploadSuccess);
        gameManager.uiManager.loading.SetActive(false);
    }

    void ShowLoginPopup() {
        
    }

    void onLoginSuccess() {
       
        save.SaveLogin(loginType);
        save.onLoginSuccess.RemoveListener(onLoginSuccess);

        if (save.data != null) {
            print("save data is not null");
            save.onUploadSuccess.AddListener(onUploadSuccess);
         
        } else {
            print("save data is null");
        }
    }

    #endregion //-------------------------------------------------------------------

    public void SelectLanguage() {

    }

    public void Contact() {

    }

    public void ClosePanel() {
        closeBtn.onClick.RemoveAllListeners();
        fadeBg.DOFade(0, time);
        panel.DOAnchorPosY(1950, time).SetEase(moveOut).OnComplete(() => {
            gameManager.uiManager.menu.AddOnClickSetting();
            gameObject.SetActive(false);
        });
    }
}
