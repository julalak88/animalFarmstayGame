using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyController : MonoBehaviour
{
    public TextMeshProUGUI coinText, expText;

    [HideInInspector]
    public RectTransform collectPos;

    SaveManager save;
    Tween twM, twH;

    private void Awake() {
        collectPos = transform.Find("CollectPos").rectTransform();
    }

    private void Start() {
        save = SaveManager.Ins;
        coinText.text = save.data.coin.ToString();
        expText.text = save.data.exp.ToString();
     
    }

    public void AddCoin(int value) {
    
        save.data.coin += value;
        coinText.text = save.data.coin.ToString();
    }

    public void RemoveCoin(int value) {
        save.data.coin -= value;
        coinText.text = save.data.coin.ToString();
    }

    public void RemoveExpVeg(float value)
    {
        save.data.expVeg -= value;
        
    }

    public void AddExpVeg(float value)
    {

        save.data.expVeg += value;
        
    }

    public void AddExp(float value) {
     
        save.data.exp += value;
        expText.text = save.data.exp.ToString();
    }
}
