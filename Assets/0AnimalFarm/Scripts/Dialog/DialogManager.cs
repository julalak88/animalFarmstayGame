using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UnityEngine.Events;
using System;
using TMPro;
public class DialogManager : MonoBehaviour
{

    public Image bgFade, imageRight,imageLeft, bgText;
    public LanguageSelector text;
    public GameObject next;
    public UnityAction OnDialogComplete;
    [HideInInspector]
    public Image currentImage;
    string[] messages;
    int index, ind;
    char[] _char;
    int char_length;
    bool isFinished = false;
    Vector3 sc;

    private void Awake() {
       
        gameObject.SetActive(false);
        next.SetActive(false);
    }

    public void OnNext() {
        if (!next.activeInHierarchy || isFinished) return;
        CheckMessage();
    }

    public void AddText(string msg) {
        messages = msg.Split(new string[] { "&"}, StringSplitOptions.None);
    }

    public void ShowDialog(Sprite character,string dir = "right") {
        if (messages.Length == 0) return;
        if (dir.Equals("right"))
        {
            imageRight.enabled = true;
            currentImage = imageRight;
            imageLeft.enabled = false;
        } else if (dir.Equals("left"))
        {
            imageLeft.enabled = true;
            currentImage = imageLeft;
            imageRight.enabled = false;
        }
            
        sc = currentImage.transform.localScale;
        gameObject.SetActive(true);
        isFinished = false;
        text.Text = "";
        currentImage.sprite = character;
        currentImage.SetNativeSize();
        bgFade.color = new Color(0, 0, 0, 0);
        currentImage.color = new Color(1, 1, 1, 0);
        bgText.color = new Color(1, 1, 1, 0);
        bgFade.DOFade(0.3f, 1f);
        bgText.DOFade(1f, 1f).SetDelay(1f);
        currentImage.DOFade(1f, 1f).SetDelay(2f).OnComplete(CheckMessage);
        next.SetActive(false);
        index = 0;
    }

    public void HideDialog() {
        text.Text = "";
       
            bgFade.DOFade(0, .5f);
            currentImage.DOFade(0, .5f);
        
        bgText.DOFade(0, .5f).OnComplete(onHideDialogComplete);
        next.SetActive(false);
    }

    void onHideDialogComplete() {
        
        gameObject.SetActive(false);
         
        OnDialogComplete.Invoke();
    }

    void CheckMessage() {
        if (index < messages.Length) {
            text.Text = "";
            next.SetActive(false);
            _char = messages[index].ToCharArray();
            char_length = _char.Length;
            index++;
            ind = 0;
            currentImage.transform.localScale = sc;
            currentImage.transform.DOScaleY(sc.y+.1f, .1f).SetLoops(6, LoopType.Yoyo);
            RevealText();
        } else {
            isFinished = true;
            HideDialog();
        }
    }

    void RevealText() {
        if (ind < char_length) {
            text.Text += _char[ind];
            ind++;
            DOVirtual.DelayedCall(.01f, RevealText);
        } else {
            next.SetActive(true);
        }
    }
}
