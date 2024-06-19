using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button facebookButton, googleButton, guestButton;

    SaveManager saveManager;

    string loginType = "";

    private void Start() {

        SoundManager.Ins.PlayBGM("BGM1");

        saveManager = SaveManager.Ins;

        if (string.IsNullOrEmpty(saveManager.lang)) {
            if (Application.systemLanguage == SystemLanguage.Thai) saveManager.lang = "TH";
            else saveManager.lang = "US";
        }
        
        if (saveManager.data == null) {

            saveManager.onLoginSuccess.AddListener(onLoginSuccess);

            print("saveManager.login " + saveManager.login);
            if (String.IsNullOrEmpty(saveManager.login)) {

                facebookButton.onClick.AddListener(onFacebookClicked);
                googleButton.onClick.AddListener(onGoogleClicked);
                guestButton.onClick.AddListener(onPlayAsGuest);

            } else {

                facebookButton.gameObject.SetActive(false);
                googleButton.gameObject.SetActive(false);
                guestButton.gameObject.SetActive(false);

                if (saveManager.login == "fb") {
                    onFacebookClicked();
                } else
                    onGoogleClicked();
            }

        }else {

            SceneManager.LoadScene("Game");

        }

    }

    private void onFacebookClicked() {
        facebookButton.onClick.RemoveListener(onFacebookClicked);
        googleButton.onClick.RemoveListener(onGoogleClicked);
        guestButton.onClick.RemoveListener(onPlayAsGuest);
        loginType = "fb";
       
    }
    private void onFacebookInit() {
       
    }

    private void onGoogleClicked() {
        facebookButton.onClick.RemoveListener(onFacebookClicked);
        googleButton.onClick.RemoveListener(onGoogleClicked);
        guestButton.onClick.RemoveListener(onPlayAsGuest);
        loginType = "google";
      
    }

    void onPlayAsGuest() {
        facebookButton.onClick.RemoveListener(onFacebookClicked);
        googleButton.onClick.RemoveListener(onGoogleClicked);
        guestButton.onClick.RemoveListener(onPlayAsGuest);
        loginType = "";
        saveManager.SetNewData();
        saveManager.SaveLogin(loginType);
        SceneManager.LoadScene("Game");
    }

    private void onLoginSuccess() {
        saveManager.SaveLogin(loginType);
        saveManager.onLoginSuccess.RemoveListener(onLoginSuccess);
        facebookButton.gameObject.SetActive(false);
        googleButton.gameObject.SetActive(false);
        guestButton.gameObject.SetActive(false);

        if (saveManager.data == null) {
            print("save data is null");
            saveManager.onDownloadSuccess.AddListener(onDownloadSaveSuccess);
          
        }else {
            print("save data is not null");
            SceneManager.LoadScene("Game");
        }
    }

    private void onDownloadSaveSuccess(bool success) {
        print("download save data from firebase "+ success);
        saveManager.onDownloadSuccess.RemoveListener(onDownloadSaveSuccess);
        if (!success) saveManager.SetNewData();
        SceneManager.LoadScene("Game");
    }
}
