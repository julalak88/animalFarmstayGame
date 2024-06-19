using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class ItemsNewController : MonoBehaviour
{
    public List<NewItem> newItems = new List<NewItem>();
    public Text valueText;
    public Text nameText;
    public Image image;
    public Image fadeBg;
    public GameObject box;
    public float timeFade;
    public float time;
    public Ease scaleIn = Ease.Flash;
    public Ease scaleOut = Ease.Flash;
    GameManager gameManager;
    int index = -1;

    private void OnEnable() {
        fadeBg.DOFade(0, 0.1f);
        fadeBg.DOFade(0.4f, timeFade);
        box.transform.localScale = Vector3.zero;
        box.transform.DOScale(1, time).SetEase(scaleIn);
    }

    private void Awake() {
        gameManager = gameManager = GameManager.Ins;
  
    }

    public void ShowFoodUnlock(List<string> unlockName) {
        if (gameManager == null) gameManager = GameManager.Ins;
        List<FoodDatabase> foods = new List<FoodDatabase>();
        foods = gameManager.database.foods.Where(x => unlockName.Contains(x.id)).Select(x => x).ToList();
        for (int i = 0; i < foods.Count; i++) {
            NewItem newItem = new NewItem();
            newItem.name = foods[i].name;
            newItem.image = foods[i].image;
            newItems.Add(newItem);
        }
        SoundManager.Ins.PlaySFX("NewItem");
        Accept();
    }

    void SetupBox() {
        nameText.text = newItems[index].name;
        image.sprite = newItems[index].image;
        valueText.text = (index+1).ToString() + " / " + newItems.Count;

    }

    public void Accept() {
        if (index == newItems.Count-1) {
           box.transform.DOScale(0, time).SetEase(scaleOut).OnComplete(Clear);
        } else {
            if (index < newItems.Count - 1) {
                index++;
                SetupBox();
            }
        }
    }


    void Clear() {
        index = -1;
        newItems.Clear();
        gameObject.SetActive(false);
    }
}
